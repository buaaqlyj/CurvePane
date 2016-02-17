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
        private List<BSplineBasisFunctionIntervalPolynomialCurve> calculateBasisFunction(bSplineCurveParam curveParam)
        {
            Dynamic2DArray<BSplineBasisFunctionIntervalPolynomialCurve> basisFunctions = new Dynamic2DArray<BSplineBasisFunctionIntervalPolynomialCurve>();
            int total = curveParam.Degree + curveParam.PointList.Count;
            BSplineBasisFunctionIntervalPolynomialCurve item = null;
            for (int i = 0; i < total; i++)
            {
                basisFunctions.SetArrayElement(0, i, new BSplineBasisFunctionIntervalPolynomialCurve(i, curveParam.Interval));
            }
            for (int i = 1; i <= curveParam.Degree; i++)
            {
                for (int j = 0; j < total - i; j++)
                {
                    //TODO: B样条基函数计算
                    //item = ;
                    basisFunctions.SetArrayElement(i, j, item);
                }
            }
            return basisFunctions[curveParam.Degree];
        }
        #endregion
    }
}
