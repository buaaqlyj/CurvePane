/// Copyright 2016 Troy Lewis. Some Rights Reserved
/// 
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
/// 
///     http://www.apache.org/licenses/LICENSE-2.0
/// 
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using CurveBase.CurveData.CurveParamData;
using CurveBase.CurveElement.IntervalPolynomialCurve;
using Util.Variable;
using Util.Variable.Interval;
using Util.Variable.PointList;

namespace CurveBase.CurveElement.ParametricCurve
{
    public class BSplineCurve : ParametricCurve
    {
        protected NormalCurvePointList pointList = null;
        protected List<BSplineBasisFunctionIntervalPolynomialCurve> basisFunctions;
        protected DataInterval interval = null;
        
        #region Constructor
        public BSplineCurve(bSplineCurveParam curveParam)
        {
            this.pointList = curveParam.PointList;
            this.basisFunctions = calculateBasisFunction(curveParam);
            this.interval = new DataInterval(curveParam.Interval.CutPoints[curveParam.Degree], curveParam.Interval.CutPoints[curveParam.PointList.Count]);
        }
        #endregion

        #region ParametricCurve Member
        public override DataPoint calculatePoint(DoubleExtension doubleExtension)
        {
            Debug.Assert(doubleExtension <= interval.RightBorder && doubleExtension >= interval.LeftBorder, "Invalid argument for BSplineCurve.calculatePoint()");
            double xVal = 0;
            double yVal = 0;
            double basisFunction = 1;
            for (int i = 0; i < pointList.Count; i++)
            {
                basisFunction = basisFunctions[i].calculate(doubleExtension).AccurateValue;
                xVal += basisFunction * pointList[i].X.AccurateValue;
                yVal += basisFunction * pointList[i].Y.AccurateValue;
            }
            return new DataPoint(xVal, yVal); ;
        }
        #endregion

        #region Private.Method
        /// <summary>
        /// 要求的：N+1 = curveParam.PointList.Count;
        /// 要求的：  k = curveParam.Degree;
        ///   k = i
        ///   i = j
        ///                    t - t[j]                              t[i + j] - t
        /// N[i, j](t) = --------------------- * N[i - 1, j](t) + --------------------- * N[i - 1, j + 1](t)
        ///               t[i + j - 1] - t[j]                      t[i + j] - t[j + 1]
        /// </summary>
        /// <param name="curveParam"></param>
        /// <returns></returns>
        private List<BSplineBasisFunctionIntervalPolynomialCurve> calculateBasisFunction(bSplineCurveParam curveParam)
        {
            Dynamic2DArray<BSplineBasisFunctionIntervalPolynomialCurve> basisFunctions = new Dynamic2DArray<BSplineBasisFunctionIntervalPolynomialCurve>();
            int total = curveParam.Degree + curveParam.PointList.Count;
            DoubleExtension denominator10, denominator20, numerator11, numerator10, numerator21, numerator20;
            numerator11 = new DoubleExtension(1);
            numerator21 = new DoubleExtension(-1);
            for (int i = 0; i < total; i++)
            {
                basisFunctions.SetArrayElement(0, i, new BSplineBasisFunctionIntervalPolynomialCurve(i, curveParam.Interval));
            }
            for (int i = 1; i <= curveParam.Degree; i++)
            {
                for (int j = 0; j < total - i; j++)
                {
                    denominator10 = curveParam.Interval.CutPoints[i + j - 1] - curveParam.Interval.CutPoints[j];
                    denominator20 = curveParam.Interval.CutPoints[i + j] - curveParam.Interval.CutPoints[j + 1];
                    numerator10 = 0 - curveParam.Interval.CutPoints[j];
                    numerator20 = curveParam.Interval.CutPoints[i + j];
                    basisFunctions.SetArrayElement(i, j, basisFunctions.GetArrayElement(i - 1, j).DivideByNumber(denominator10).MultiplyByLinear(numerator11, numerator10) + basisFunctions.GetArrayElement(i - 1, j + 1).DivideByNumber(denominator20).MultiplyByLinear(numerator21, numerator20));
                }
            }
            return basisFunctions[curveParam.Degree];
        }
        #endregion
    }
}
