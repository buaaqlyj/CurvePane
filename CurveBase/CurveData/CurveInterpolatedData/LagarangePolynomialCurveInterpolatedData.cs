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
using Util.Tool;
using Util.Variable;
using Util.Variable.PointList;

namespace CurveBase.CurveData.CurveInterpolatedData
{
    public class LagarangePolynomialCurveInterpolatedData : ICurveInterpolatedData
    {
        private PiecewiseIntervalPolynomialCurveElement curve = null;
        private OrderedCurvePointList lagarangePolynomialInterpolatedPoints = null;
        private PolynomialCurveType curveType;

        #region Constructor
        public LagarangePolynomialCurveInterpolatedData(PolynomialCurveParam curveParam)
        {
            this.curveType = curveParam.PolynomialCurveType;
            switch (curveType)
            {
                case PolynomialCurveType.Lagrange_Linear:
                    lagarangePolynomialInterpolatedPoints = curveParam.PointList;
                    break;
                case PolynomialCurveType.Lagrange_Quadratic:
                    curve = generateQuadraticPolynomialCurves(curveParam);
                    break;
            }
        }
        #endregion

        #region Property
        public PiecewiseIntervalPolynomialCurveElement Curves
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
            return curve.LastPoint;
        }
        #endregion

        #region Private.Methods
        private PiecewiseIntervalPolynomialCurveElement generateQuadraticPolynomialCurves(PolynomialCurveParam param)
        {
            Debug.Assert(param.PolynomialCurveType == PolynomialCurveType.Lagrange_Quadratic, @"This method """"generateQuadraticPolynomialCurves"""" only supports quadratic polynomialCurveType");
            OrderedCurvePointList pointList = param.PointList;
            List<NormalIntervalPolynomialCurveElement> polynomialCurve = new List<NormalIntervalPolynomialCurveElement>();
            List<DoubleExtension> coefficients = null;
            List<DoubleExtension> cutPoints = new List<DoubleExtension>();
            cutPoints.Add(pointList[0].X);
            for (int i = 2; i < pointList.Count; i = i + 2)
            {
                coefficients = MathExtension.calculateQuadraticPolynomialCoefficients(pointList[i - 2], pointList[i - 1], pointList[i]);
                polynomialCurve.Add(new NormalIntervalPolynomialCurveElement(coefficients, 3, pointList[i - 2].X, pointList[i].X));
                cutPoints.Add(pointList[i].X);
            }
            return new PiecewiseIntervalPolynomialCurveElement(polynomialCurve, cutPoints);
        }
        #endregion
    }
}
