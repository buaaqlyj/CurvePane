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

using CurveBase.CurveData.CurveParam;
using CurveBase.CurveElement.IntervalPolynomialCurve;
using Util.Tool;
using Util.Variable;
using Util.Variable.Interval;
using Util.Variable.Matrix;
using Util.Variable.PointList;

namespace CurveBase.CurveData.CurveInterpolatedData
{
    public class LagarangePolynomialCurveInterpolatedData : ICurveInterpolatedData
    {
        private NormalIntervalPolynomialCurveElement curve = null;
        private OrderedCurvePointList lagarangePolynomialInterpolatedPoints = null;
        private PolynomialCurveType curveType;
        private DataPoint lastPoint = null;
        private int count = 0;

        #region Constructor
        public LagarangePolynomialCurveInterpolatedData(PolynomialCurveParam curveParam)
        {
            this.curveType = curveParam.PolynomialCurveType;
            this.lastPoint = curveParam.PointList.RightBorderPoint;
            this.count = curveParam.Count;
            this.curve = GenerateCurve(curveParam);
        }
        #endregion

        #region Property
        public NormalIntervalPolynomialCurveElement Curve
        {
            get
            {
                return curve;
            }
        }

        public OrderedCurvePointList PointList
        {
            get
            {
                return lagarangePolynomialInterpolatedPoints;
            }
        }
        #endregion

        #region ICurveInterpolatedData Member
        public InterpolationCurveType getCurveType()
        {
            return InterpolationCurveType.polynomialCurve;
        }

        public DataPoint getLastPoint()
        {
            return lastPoint;
        }
        #endregion

        #region Private.Methods
        private NormalIntervalPolynomialCurveElement GenerateCurve(PolynomialCurveParam curveParam)
        {
            Debug.Assert(curveParam.PolynomialCurveType == PolynomialCurveType.Lagrange, @"This interpolated method """"GenerateCurve"""" only supports Lagarange Polynomial Curve.");
            double[,] coefficientsArray = new double[count, count];
            double[,] constantArray = new double[count, 1];
            double coef, x;
            
            for (int i = 0; i < count; i++)
            {
                constantArray[i, 0] = curveParam[i].Y.AccurateValue;
                coef = 1;
                x = curveParam[i].X.AccurateValue;
                for (int j = 0; j < count; j++)
                {
                    coefficientsArray[i, j] = coef;
                    coef *= x;
                }
            }

            LinearEquationSet equations = new LinearEquationSet(new Matrix(coefficientsArray), new Matrix(constantArray));
            Matrix result = equations.AnswerMatrix;

            List<DoubleExtension> coefs = new List<DoubleExtension>();
            for (int i = 0; i < count; i++)
            {
                coefs.Add(new DoubleExtension(result[i, 0]));
            }
            return new NormalIntervalPolynomialCurveElement(coefs, count, new DataInterval(curveParam.PointList.LeftBorderPoint.X, curveParam.PointList.RightBorderPoint.X)); 
            //OrderedCurvePointList pointList = param.PointList;
            //List<NormalIntervalPolynomialCurveElement> polynomialCurve = new List<NormalIntervalPolynomialCurveElement>();
            //List<DoubleExtension> coefficients = null;
            //List<DoubleExtension> cutPoints = new List<DoubleExtension>();
            //cutPoints.Add(pointList[0].X);
            //for (int i = 2; i < pointList.Count; i = i + 2)
            //{
            //    coefficients = MathExtension.CalculateQuadraticPolynomialCoefficients(pointList[i - 2], pointList[i - 1], pointList[i]);
            //    polynomialCurve.Add(new NormalIntervalPolynomialCurveElement(coefficients, 3, pointList[i - 2].X, pointList[i].X));
            //    cutPoints.Add(pointList[i].X);
            //}
            //return new PiecewiseIntervalPolynomialCurveElement(polynomialCurve, cutPoints);
        }
        #endregion
    }
}
