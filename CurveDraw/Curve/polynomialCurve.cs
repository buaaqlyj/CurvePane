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
using System.Drawing;
using System.Text;

using CurveBase;
using CurveBase.CurveElements;
using CurveBase.CurveElements.IntervalCurve;
using CurveBase.CurveException;
using CurveBase.CurveData.CurveParamData;
using CurveBase.CurveData.CurveInterpolatedData;
using Util.Tool;
using Util.Variable;
using Util.Variable.PointList;

namespace CurveDraw.Curve
{
    public class polynomialCurve : ICurve
    {
        private polynomialCurveParam curveParam;
        private bool canDraw = false;

        #region ICurve Member

        public polynomialCurve(ICurveParam curveParam)
        {
            if (canDrawCurve(curveParam))
            {
                canDraw = true;
                this.curveParam = (polynomialCurveParam)curveParam;
            }
        }

        public Dictionary<ICurvePointList, DrawType> sampleCurvePoints()
        {
            polynomialCurveInterpolatedData data = interpolateCurve();
            OrderedCurvePointList list = new OrderedCurvePointList();
            Dictionary<ICurvePointList, DrawType> result = new Dictionary<ICurvePointList, DrawType>();
            if (data.PointList != null)
            {
                foreach (Util.Variable.DataPoint item in data.PointList)
                {
                    list.Add(item);
                }
            }
            else if (data.Curves != null)
            {
                foreach (PolynomialCurve curve in data.Curves)
                {
                    list.AddRange(sampleACurve(curve));
                }
                list.Add(data.getLastPoint());
            }
            list.Label = "P";
            result.Add(list, DrawType.LineNoDot);
            return result;
        }

        public bool CanDraw
        {
            get { return canDraw; }
        }
        #endregion

        #region Private.Methods
        private List<PolynomialCurve> generatePolynomialCurves(polynomialCurveParam param)
        {
            polynomialCurveType curveType = param.PolynomialCurveType;
            OrderedCurvePointList pointList = param.PointList;
            List<PolynomialCurve> polynomialCurve = new List<PolynomialCurve>();
            List<double> coefficients;
            switch (curveType)
            {
                case polynomialCurveType.Lagrange_Linear:
                    for (int i = 1; i < pointList.Count; i++)
                    {
                        coefficients = MathExtension.calculateLinearPolynomialCoefficients(pointList[i - 1], pointList[i]);
                        polynomialCurve.Add(new PolynomialCurve(coefficients, 2, pointList[i - 1].X, pointList[i].X));
                    }
                    break;
                case polynomialCurveType.Lagrange_Quadratic:
                    for (int i = 2; i < pointList.Count; i = i + 2)
                    {
                        coefficients = MathExtension.calculateQuadraticPolynomialCoefficients(pointList[i - 2], pointList[i - 1], pointList[i]);
                        polynomialCurve.Add(new PolynomialCurve(coefficients, 3, pointList[i - 2].X, pointList[i].X));
                    }
                    break;
                case polynomialCurveType.Newton:
                    //TODO: generate Polynomial Curves for Newton
                    break;
            }
            return polynomialCurve;
        }

        private bool canDrawCurve(ICurveParam curveParam)
        {
            if (curveParam.getCurveType() == CurveType.polynomialCurve)
            {
                polynomialCurveParam param = (polynomialCurveParam)curveParam;
                if (!param.PointList.noDuplicatedX())
                    throw new SameXInOrderedCurvePointListException(CurveType.polynomialCurve);
            }
            else
            {
                throw new UnmatchedCurveParamTypeException(CurveType.polynomialCurve, curveParam.getCurveType());
            }
            return true;
        }

        private polynomialCurveInterpolatedData interpolateCurve()
        {
            if (curveParam.PolynomialCurveType == polynomialCurveType.Lagrange_Linear)
            {
                return new polynomialCurveInterpolatedData(curveParam.PointList, polynomialCurveType.Lagrange_Linear);
            }
            return new polynomialCurveInterpolatedData(generatePolynomialCurves(curveParam), curveParam.PolynomialCurveType);
        }

        private List<DataPoint> sampleACurve(PolynomialCurve curve)
        {
            double stepSize;
            int step;
            if (curve.Interval.Length.CoordinateValue > 0.05)
            {
                stepSize = curve.Interval.Length.CoordinateValue / 50;
                step = 50;
            }
            else
            {
                stepSize = 0.001;
                step = (int)(curve.Interval.Length.CoordinateValue * 1000);
            }
            double xValue = curve.Interval.LeftBorder.CoordinateValue;
            List<DataPoint> pts = new List<DataPoint>();
            int count = 0;
            while (count < step)
            {
                pts.Add(new DataPoint(xValue, curve.calculate(xValue)));
                xValue += stepSize;
                count++;
            }
            return pts;
        }
        #endregion



    }
}