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

using CurveBase.CurveData.CurveParam;
using CurveBase.CurveElement.IntervalPolynomialCurve;
using CurveBase.CurveException;
using Util.Variable;
using Util.Variable.Interval;
using Util.Variable.PointList;

namespace CurveBase.CurveElement.ParametricCurve
{
    public class NurbsParametricCurveElement : BSplineParametricCurveElement
    {
        private List<DoubleExtension> weights = null;

        #region Constructor
        public NurbsParametricCurveElement(NurbsCurveParam curveParam)
            : base(curveParam)
        {
            this.weights = curveParam.Weight;
        }
        #endregion

        #region ParametricCurveElement Member
        public override DataPoint calculatePoint(DoubleExtension doubleExtension)
        {
            Debug.Assert(interval.isBetweenBordersCloseInterval(doubleExtension), "Invalid argument for NurbsParametricCurveElement.calculatePoint()");
            DoubleExtension xVal = new DoubleExtension(0);
            DoubleExtension yVal = new DoubleExtension(0);
            DoubleExtension basisFunction;
            DoubleExtension denominator = new DoubleExtension(0);
            for (int i = 0; i < pointList.Count; i++)
            {
                basisFunction = basisFunctions[i].calculate(doubleExtension) * weights[i];
                xVal += basisFunction * pointList[i].X;
                yVal += basisFunction * pointList[i].Y;
                denominator += basisFunction.AccurateValue;
            }
            return new DataPoint(xVal / denominator, yVal / denominator);
        }
        #endregion

        #region Private.Method
        protected override void calculateBasisFunction(ICurveParam curveParam)
        {
            if (curveParam.getCurveType() == InterpolationCurveType.nurbsCurve)
            {
                NurbsCurveParam param = (NurbsCurveParam)curveParam;
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
                throw new UnmatchedCurveParamTypeException(InterpolationCurveType.nurbsCurve, curveParam.getCurveType());
            }
        }
        #endregion
    }
}
