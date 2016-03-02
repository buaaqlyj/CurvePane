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
using Util.Variable;
using Util.Variable.Interval;

namespace CurveBase.CurveElement.ParametricCurve
{
    public class ParametricCubicSplineInterpolationCurveElement : ParametricCurveElement
    {
        protected PiecewiseIntervalPolynomialCurveElement xCurve, yCurve;

        #region Constructor
        public ParametricCubicSplineInterpolationCurveElement(PiecewiseIntervalPolynomialCurveElement xCurve, PiecewiseIntervalPolynomialCurveElement yCurve)
        {
            if (!xCurve.Interval.Equals(yCurve.Interval))
                throw new ArgumentException("The X and Y piecewise curve should have the same intervals.");
            this.interval = xCurve.Interval;
            this.xCurve = xCurve;
            this.yCurve = yCurve;
        }
        #endregion

        #region ParametricCurveElement Member
        public override DataPoint calculatePoint(DoubleExtension doubleExtension)
        {
            Debug.Assert(doubleExtension <= interval.RightBorder && doubleExtension >= interval.LeftBorder, "The parameter is out of range for ParametricCubicSplineInterpolationCurve.");
            return new DataPoint(xCurve.calculate(doubleExtension), yCurve.calculate(doubleExtension));
        }
        #endregion
    }
}
