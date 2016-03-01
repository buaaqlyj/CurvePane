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
using System.Text;

using CurveBase.CurveData.CurveParam;
using CurveBase.CurveElement.IntervalPolynomialCurve;
using Util.Tool;
using Util.Variable;
using Util.Variable.Matrix;
using Util.Variable.PointList;

namespace CurveBase.CurveData.CurveInterpolatedData
{
    public class CubicSplineInterpolationInterpolatedData : ICurveInterpolatedData
    {
        private List<LagarangeIntervalPolynomialCurveElement> curves = null;
        private CubicSplineInterpolationCurveParam curveParam;

        #region Constructor
        public CubicSplineInterpolationInterpolatedData(CubicSplineInterpolationCurveParam curveParam)
        {
            this.curveParam = curveParam;
            this.curves = GenerateCurves(curveParam);
        }
        #endregion

        #region Property
        public List<LagarangeIntervalPolynomialCurveElement> Curves
        {
            get
            {
                return curves;
            }
        }
        #endregion

        #region ICurveInterpolatedData Member
        public CurveType getCurveType()
        {
            return CurveType.csiCurve;
        }

        public DataPoint getLastPoint()
        {
            return curveParam.PointList.RightBorderPoint;
        }
        #endregion

        #region Private.Methods
        private List<LagarangeIntervalPolynomialCurveElement> GenerateCurves(CubicSplineInterpolationCurveParam curveParam)
        {
            double[,] coefficientsArray = new double[curveParam.Count, curveParam.Count];
            double[,] constantArray = new double[curveParam.Count, 1];
            double uVal, dVal, val1, val2;
            for (int i = 1; i < curveParam.Count - 1; i++)
            {
                uVal = (curveParam[i].X.AccurateValue - curveParam[i - 1].X.AccurateValue) / (curveParam[i + 1].X.AccurateValue - curveParam[i - 1].X.AccurateValue);
                dVal = 6 / (curveParam[i + 1].X.AccurateValue - curveParam[i - 1].X.AccurateValue) * (
                    (curveParam[i + 1].Y.AccurateValue - curveParam[i].Y.AccurateValue) / (curveParam[i + 1].X.AccurateValue - curveParam[i].X.AccurateValue) - 
                    (curveParam[i].Y.AccurateValue - curveParam[i - 1].Y.AccurateValue) / (curveParam[i].X.AccurateValue - curveParam[i - 1].X.AccurateValue)
                    );
                coefficientsArray[i, i - 1] = uVal;
                coefficientsArray[i, i] = 2;
                coefficientsArray[i, i + 1] = 1 - uVal;
                constantArray[i, 0] = dVal;
            }
            switch (curveParam.BorderConditionType)
            {
                case CSIBorderConditionType.First_Order_Derivative:
                    val1 = 6 / (curveParam[2].X.AccurateValue - curveParam[1].X.AccurateValue) * (
                        (curveParam[1].Y.AccurateValue - curveParam[0].Y.AccurateValue) / (curveParam[1].X.AccurateValue - curveParam[0].X.AccurateValue) -
                        curveParam.LeftBorderValue.AccurateValue
                        );
                    val2 = 6 / (curveParam[curveParam.Count - 1].X.AccurateValue - curveParam[curveParam.Count - 2].X.AccurateValue) * (
                        curveParam.RightBorderValue.AccurateValue -
                        (curveParam[curveParam.Count - 1].Y.AccurateValue - curveParam[curveParam.Count - 2].Y.AccurateValue) / (curveParam[curveParam.Count - 1].X.AccurateValue - curveParam[curveParam.Count - 2].X.AccurateValue)
                        );
                    coefficientsArray[0, 0] = 2;
                    coefficientsArray[0, 1] = 1;
                    coefficientsArray[curveParam.Count - 1, curveParam.Count - 2] = 1;
                    coefficientsArray[curveParam.Count - 1, curveParam.Count - 1] = 2;
                    constantArray[0, 0] = val1;
                    constantArray[curveParam.Count - 1, 0] = val2;
                    break;
                case CSIBorderConditionType.Second_Order_Derivative:
                    coefficientsArray[0, 0] = 1;
                    coefficientsArray[curveParam.Count - 1, curveParam.Count - 1] = 1;
                    constantArray[0, 0] = curveParam.LeftBorderValue.AccurateValue;
                    constantArray[curveParam.Count - 1, 0] = curveParam.RightBorderValue.AccurateValue;
                    break;
                case CSIBorderConditionType.Cyclicity:
                    val1 = curveParam[1].X.AccurateValue - curveParam[0].X.AccurateValue;
                    coefficientsArray[0, 0] = val1;
                    coefficientsArray[0, 1] = 2 * val1;
                    val1 = curveParam[curveParam.Count - 1].X.AccurateValue - curveParam[curveParam.Count - 2].X.AccurateValue;
                    coefficientsArray[0, curveParam.Count - 2] = val1;
                    coefficientsArray[0, curveParam.Count - 1] = 2 * val1;
                    coefficientsArray[curveParam.Count - 1, 0] = 1;
                    coefficientsArray[curveParam.Count - 1, curveParam.Count - 1] = -1;
                    val2 = 6 * (
                        (curveParam[1].Y.AccurateValue - curveParam[0].Y.AccurateValue) / (curveParam[1].X.AccurateValue - curveParam[0].X.AccurateValue) - 
                        (curveParam[curveParam.Count - 1].Y.AccurateValue - curveParam[curveParam.Count - 2].Y.AccurateValue) / (curveParam[curveParam.Count - 1].X.AccurateValue - curveParam[curveParam.Count - 2].X.AccurateValue)
                        );
                    constantArray[0, 0] = val2;
                    break;
            }
            LinearEquationSet equations = new LinearEquationSet(new Matrix(coefficientsArray), new Matrix(constantArray));
            Matrix result = equations.AnswerMatrix;
            List<LagarangeIntervalPolynomialCurveElement> curves = new List<LagarangeIntervalPolynomialCurveElement>();
            for (int i = 1; i < curveParam.Count; i++)
            {
                curves.Add(new LagarangeIntervalPolynomialCurveElement(result[i - 1, 0], result[i, 0], curveParam[i - 1], curveParam[i]));
            }
            return curves;
        }
        #endregion
    }
}
