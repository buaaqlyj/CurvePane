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

using CurveBase.CurveData.CurveParamData;
using CurveBase.CurveElement.IntervalPolynomialCurve;
using Util.Tool;
using Util.Variable;
using Util.Variable.PointList;

namespace CurveBase.CurveData.CurveInterpolatedData
{
    public class LagarangePolynomialCurveInterpolatedData : ICurveInterpolatedData
    {
        private List<LagarangeIntervalPolynomialCurve> lagarangePolynomialInterpolatedCurves = null;
        private OrderedCurvePointList lagarangePolynomialInterpolatedPoints = null;
        private polynomialCurveType curveType;

        #region Constructor
        public LagarangePolynomialCurveInterpolatedData(polynomialCurveParam curveParam)
        {
            this.curveType = curveParam.PolynomialCurveType;
            switch (curveType)
            {
                case polynomialCurveType.Lagrange_Linear:
                    lagarangePolynomialInterpolatedPoints = curveParam.PointList;
                    break;
                case polynomialCurveType.Lagrange_Quadratic:
                    lagarangePolynomialInterpolatedCurves = generateQuadraticPolynomialCurves(curveParam);
                    break;
            }
        }
        #endregion

        #region Property
        public List<LagarangeIntervalPolynomialCurve> Curves
        {
            get
            {
                return lagarangePolynomialInterpolatedCurves;
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
        public CurveType getCurveType()
        {
            return CurveType.polynomialCurve;
        }

        public DataPoint getLastPoint()
        {
            return lagarangePolynomialInterpolatedCurves[lagarangePolynomialInterpolatedCurves.Count - 1].LastPoint;
        }
        #endregion

        #region Private.Methods

        private List<LagarangeIntervalPolynomialCurve> generateQuadraticPolynomialCurves(polynomialCurveParam param)
        {
            Debug.Assert(param.PolynomialCurveType == polynomialCurveType.Lagrange_Quadratic, @"This method """"generateQuadraticPolynomialCurves"""" only supports quadratic polynomialCurveType");
            OrderedCurvePointList pointList = param.PointList;
            List<LagarangeIntervalPolynomialCurve> polynomialCurve = new List<LagarangeIntervalPolynomialCurve>();
            List<double> coefficients = null;
            for (int i = 2; i < pointList.Count; i = i + 2)
            {
                coefficients = MathExtension.calculateQuadraticPolynomialCoefficients(pointList[i - 2], pointList[i - 1], pointList[i]);
                polynomialCurve.Add(new LagarangeIntervalPolynomialCurve(coefficients, 3, pointList[i - 2].X, pointList[i].X));
            }
            return polynomialCurve;
        }

        #endregion
    }
}
