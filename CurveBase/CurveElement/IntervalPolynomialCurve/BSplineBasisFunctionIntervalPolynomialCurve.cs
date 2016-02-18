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
        protected bool equalsToZero = false;

        #region Constructor
        public BSplineBasisFunctionIntervalPolynomialCurve(List<LagarangeIntervalPolynomialCurve> curves, PiecewiseDataInterval interval)
        {
            if (curves.Count != interval.SubIntervals.Count)
                throw new ArgumentException("Curves' count is different from intervals' count.");
            this.polynomialCurves = curves;
            this.interval = interval;
            for (int i = 0; i < curves.Count; i++ )
            {
                if (!curves[i].EqualsToZero && !interval.SubIntervals[i].NullInterval)
                    return;
            }
            equalsToZero = true;
        }

        public BSplineBasisFunctionIntervalPolynomialCurve(int index, PiecewiseDataInterval interval)
        {
            polynomialCurves = new List<LagarangeIntervalPolynomialCurve>();
            for (int i = 0; i < interval.SubIntervals.Count; i++)
            {
                polynomialCurves.Add(new LagarangeIntervalPolynomialCurve(new DoubleExtension((i == index) ? 1 : 0), interval.SubIntervals[i]));
            }
            if (interval.SubIntervals[index].NullInterval) equalsToZero = true;
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

        public bool EqualsToZero
        {
            get
            {
                return equalsToZero;
            }
        }

        public LagarangeIntervalPolynomialCurve this[int index]
        {
            get
            {
                if (index < 0 || index >= polynomialCurves.Count)
                {
                    throw new ArgumentOutOfRangeException("index", "The index is out of range.");
                }
                return polynomialCurves[index];
            }
        }
        #endregion

        #region Public.Interface
        public BSplineBasisFunctionIntervalPolynomialCurve MultiplyByLinear(DoubleExtension val1, DoubleExtension val0)
        {
            List<LagarangeIntervalPolynomialCurve> curve = new List<LagarangeIntervalPolynomialCurve>();
            for (int i = 0; i < Curves.Count; i++)
            {
                curve.Add(Curves[i].MultiplyByLinear(val1, val0));
            }
            return new BSplineBasisFunctionIntervalPolynomialCurve(curve, Interval);
        }

        public BSplineBasisFunctionIntervalPolynomialCurve DivideByNumber(DoubleExtension val)
        {
            List<LagarangeIntervalPolynomialCurve> curve = new List<LagarangeIntervalPolynomialCurve>();
            for (int i = 0; i < Curves.Count; i++)
            {
                curve.Add(Curves[i].DivideByNumber(val));
            }
            return new BSplineBasisFunctionIntervalPolynomialCurve(curve, Interval);
        }
        #endregion

        #region
        public static BSplineBasisFunctionIntervalPolynomialCurve operator +(BSplineBasisFunctionIntervalPolynomialCurve c1, BSplineBasisFunctionIntervalPolynomialCurve c2)
        {
            if (c1.Interval.Equals(c2.Interval))
            {
                List<LagarangeIntervalPolynomialCurve> curves = new List<LagarangeIntervalPolynomialCurve>();
                for (int i = 0; i < c1.Curves.Count; i++)
                {
                    curves.Add(c1.Curves[i] + c2.Curves[i]);
                }
                return new BSplineBasisFunctionIntervalPolynomialCurve(curves, c1.Interval);
            }
            throw new ArgumentException("The two BSplineBasisFunctionIntervalPolynomialCurve have different intervals.");
        }
        #endregion
    }
}
