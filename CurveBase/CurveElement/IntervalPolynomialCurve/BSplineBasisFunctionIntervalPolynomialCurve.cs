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

using Util.Variable;
using Util.Variable.Interval;

namespace CurveBase.CurveElement.IntervalPolynomialCurve
{
    public class BSplineBasisFunctionIntervalPolynomialCurve : IntervalPolynomialCurve
    {
        protected new PiecewiseDataInterval interval;
        protected List<LagarangeIntervalPolynomialCurve> polynomialCurves;

        #region Constructor
        public BSplineBasisFunctionIntervalPolynomialCurve(List<LagarangeIntervalPolynomialCurve> curves, PiecewiseDataInterval interval)
        {
            this.polynomialCurves = curves;
            this.interval = interval;
        }

        public BSplineBasisFunctionIntervalPolynomialCurve(int index, PiecewiseDataInterval interval)
        {
            polynomialCurves = new List<LagarangeIntervalPolynomialCurve>();
            for (int i = 0; i < interval.SubIntervals.Count; i++)
            {
                polynomialCurves.Add(new LagarangeIntervalPolynomialCurve((i == index) ? 1 : 0, interval.SubIntervals[i]));
            }
            this.interval = interval;
        }
        #endregion

        #region IntervalPolynomialCurve Member
        public override DoubleExtension calculate(DoubleExtension doubleExtension)
        {
            int i = interval.findIntervalIndex(doubleExtension);
            if (i > -1)
            {
                Debug.Assert(interval[i].isBetweenBordersCloseInterval(doubleExtension), "Can't find proper interval for given value.");
                return polynomialCurves[i].calculate(doubleExtension);
            }
            else
            {
                throw new ArgumentOutOfRangeException("doubleExtension", "This parameter is out of range.");
            }
        }
        #endregion

        #region Property
        public new PiecewiseDataInterval Interval
        {
            get
            {
                return interval;
            }
        }

        public List<LagarangeIntervalPolynomialCurve> Curves
        {
            get
            {
                return polynomialCurves;
            }
        }
        #endregion

        #region Public.Interface
        public LagarangeIntervalPolynomialCurve getCurve(int index)
        {
            if (index < 0 || index >= polynomialCurves.Count)
            {
                throw new ArgumentOutOfRangeException("index", "The index is out of range.");
            }
            return polynomialCurves[index];
        }
        #endregion
    }
}
