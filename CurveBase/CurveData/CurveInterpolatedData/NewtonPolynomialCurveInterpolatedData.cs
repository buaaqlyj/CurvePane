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

using CurveBase.CurveData.CurveParamData;
using CurveBase.CurveElement.IntervalPolynomialCurve;
using Util.Variable;
using Util.Variable.PointList;

namespace CurveBase.CurveData.CurveInterpolatedData
{
    public class NewtonPolynomialCurveInterpolatedData : ICurveInterpolatedData
    {
        private NewtonIntervalPolynomialCurve newtonCurve = null;
        private OrderedCurvePointList pointList = null;

        #region Constructor
        public NewtonPolynomialCurveInterpolatedData(polynomialCurveParam curveParam)
        {
            pointList = curveParam.PointList;
            newtonCurve = new NewtonIntervalPolynomialCurve(curveParam.PointList);
        }
        #endregion

        #region Property
        public OrderedCurvePointList PointList
        {
            get
            {
                return pointList;
            }
        }

        public NewtonIntervalPolynomialCurve Curve
        {
            get
            {
                return newtonCurve;
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
            return new DataPoint(newtonCurve.Interval.RightBorder.AccurateValue, newtonCurve.calculate(newtonCurve.Interval.RightBorder.AccurateValue));
        }
        #endregion
    }
}
