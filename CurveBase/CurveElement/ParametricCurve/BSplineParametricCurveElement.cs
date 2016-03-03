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

using System.Collections.Generic;
using System.Diagnostics;

using CurveBase.CurveData.CurveParam;
using CurveBase.CurveElement.IntervalPolynomialCurve;
using CurveBase.CurveException;
using Util.Variable;
using Util.Variable.Interval;
using Util.Variable.PointList;

namespace CurveBase.CurveElement.ParametricCurve
{
    public class BSplineParametricCurveElement : ParametricCurveElement
    {
        protected NormalCurvePointList pointList = null;
        protected List<PiecewiseIntervalPolynomialCurveElement> basisFunctions;
        
        #region Constructor
        public BSplineParametricCurveElement(BSplineCurveParam curveParam)
        {
            this.pointList = curveParam.PointList;
            this.interval = new DataInterval(curveParam.Interval.CutPoints[curveParam.Degree], curveParam.Interval.CutPoints[curveParam.PointList.Count]);
            calculateBasisFunction(curveParam);
        }
        #endregion

        #region ParametricCurve Member
        public override DataPoint calculatePoint(DoubleExtension doubleExtension)
        {
            Debug.Assert(interval.IsBetweenBordersCloseInterval(doubleExtension), "Invalid argument for BSplineCurve.calculatePoint()");
            DoubleExtension xVal = new DoubleExtension(0);
            DoubleExtension yVal = new DoubleExtension(0);
            DoubleExtension basisFunction;
            for (int i = 0; i < pointList.Count; i++)
            {
                basisFunction = basisFunctions[i].calculate(doubleExtension);
                xVal += basisFunction * pointList[i].X;
                yVal += basisFunction * pointList[i].Y;
            }
            return new DataPoint(xVal, yVal);
        }
        #endregion

        #region Private.Method
        /// <summary>
        /// 要求的：N+1 = curveParam.PointList.Count;
        /// 要求的：  k = curveParam.Degree;
        ///   k = i
        ///   i = j
        ///                  t - t[j]                              t[i + j + 1] - t
        /// N[i, j](t) = ----------------- * N[i - 1, j](t) + ------------------------- * N[i - 1, j + 1](t)
        ///               t[i + j] - t[j]                      t[i + j + 1] - t[j + 1]
        ///               numerator11 * t + numerator10                      numerator21 * t + numerator20
        ///            = ------------------------------- * N[i - 1, j](t) + ------------------------------- * N[i - 1, j + 1](t)
        ///                      denominator10                                       denominator20
        /// numerator11   = 1
        /// numerator10   = -t[j]
        /// numerator21   = -1
        /// numerator20   = t[i + j + 1]
        /// denominator10 = t[i + j] - t[j]
        /// denominator20 = t[i + j + 1] - t[j + 1]
        /// </summary>
        /// <param name="curveParam"></param>
        /// <returns></returns>
        protected virtual void calculateBasisFunction(ICurveParam curveParam)
        {
            if (curveParam.getCurveType() == InterpolationCurveType.bsCurve)
            {
                BSplineCurveParam param = (BSplineCurveParam)curveParam;
                Dynamic2DArray<PiecewiseIntervalPolynomialCurveElement> basisFunctions = new Dynamic2DArray<PiecewiseIntervalPolynomialCurveElement>();
                int total = param.Degree + param.PointList.Count;
                DoubleExtension denominator10, denominator20, numerator11, numerator10, numerator21, numerator20;
                PiecewiseIntervalPolynomialCurveElement curve1, curve2;
                numerator11 = new DoubleExtension(1);
                numerator21 = new DoubleExtension(-1);
                for (int i = 0; i < total; i++)
                {
                    basisFunctions.SetArrayElement(0, i, new PiecewiseIntervalPolynomialCurveElement(i, param.Interval));
                }
                for (int i = 1; i <= param.Degree; i++)
                {
                    for (int j = 0; j < total - i; j++)
                    {
                        denominator10 = param.Interval.CutPoints[i + j] - param.Interval.CutPoints[j];
                        denominator20 = param.Interval.CutPoints[i + j + 1] - param.Interval.CutPoints[j + 1];
                        numerator10 = 0 - param.Interval.CutPoints[j];
                        numerator20 = param.Interval.CutPoints[i + j + 1];
                        curve1 = basisFunctions.GetArrayElement(i - 1, j).DivideByNumber(denominator10).MultiplyByLinear(numerator11, numerator10);
                        curve2 = basisFunctions.GetArrayElement(i - 1, j + 1).DivideByNumber(denominator20).MultiplyByLinear(numerator21, numerator20);
                        basisFunctions.SetArrayElement(i, j, curve1 + curve2);
                    }
                }
                this.basisFunctions = basisFunctions[param.Degree];
            }
            else
            {
                throw new UnmatchedCurveParamTypeException(InterpolationCurveType.bsCurve, curveParam.getCurveType());
            }
        }
        #endregion
    }
}
