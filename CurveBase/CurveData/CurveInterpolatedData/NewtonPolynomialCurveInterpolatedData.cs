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
using Util.Variable;
using Util.Variable.PointList;

namespace CurveBase.CurveData.CurveInterpolatedData
{
    public class NewtonPolynomialCurveInterpolatedData : ICurveInterpolatedData
    {
        private NewtonIntervalPolynomialCurveElement newtonCurve = null;
        private OrderedCurvePointList pointList = null;
        
        #region Constructor
        public NewtonPolynomialCurveInterpolatedData(PolynomialCurveParam curveParam)
        {
            this.pointList = curveParam.PointList;
            this.newtonCurve = new NewtonIntervalPolynomialCurveElement(pointList);
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

        public NewtonIntervalPolynomialCurveElement Curve
        {
            get
            {
                return newtonCurve;
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
            return new DataPoint(newtonCurve.Interval.RightBorder.AccurateValue, newtonCurve.calculate(newtonCurve.Interval.RightBorder).AccurateValue);
        }
        #endregion
    }
}
