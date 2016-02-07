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

using CurveBase.CurveData.CurveParamData;
using CurveBase.CurveElements.IntervalCurve;
using Util.Variable;
using Util.Variable.PointList;

namespace CurveBase.CurveData.CurveInterpolatedData
{
    public class polynomialCurveInterpolatedData : ICurveInterpolatedData
    {
        private List<PolynomialCurve> polynomialInterpolatedCurves;
        private OrderedCurvePointList polynomialInterpolatedPoints;
        private polynomialCurveType curveType;

        #region Constructor

        public polynomialCurveInterpolatedData(List<PolynomialCurve> curves, polynomialCurveType curveType)
        {
            polynomialInterpolatedCurves = curves;
            polynomialInterpolatedPoints = null;
            this.curveType = curveType;
        }

        public polynomialCurveInterpolatedData(OrderedCurvePointList points, polynomialCurveType curveType)
        {
            polynomialInterpolatedCurves = null;
            polynomialInterpolatedPoints = points;
            this.curveType = curveType;
        }
        #endregion

        #region Property
        public List<PolynomialCurve> Curves
        {
            get
            {
                return polynomialInterpolatedCurves;
            }
        }

        public OrderedCurvePointList PointList
        {
            get
            {
                return polynomialInterpolatedPoints;
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
            return polynomialInterpolatedCurves[polynomialInterpolatedCurves.Count - 1].LastPoint;
        }
        #endregion


        
    }
}
