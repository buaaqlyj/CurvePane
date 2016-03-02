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
    public class PiecewiseIntervalPolynomialCurveElement : IntervalPolynomialCurveElement
    {
        protected new PiecewiseDataInterval interval;
        protected List<NormalIntervalPolynomialCurveElement> polynomialCurves;
        protected bool equalsToZero = false;

        #region Constructor
        public PiecewiseIntervalPolynomialCurveElement(List<NormalIntervalPolynomialCurveElement> curves, PiecewiseDataInterval interval)
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
        
        public PiecewiseIntervalPolynomialCurveElement(List<NormalIntervalPolynomialCurveElement> curves, List<DoubleExtension> cutPoints)
            : this(curves, new PiecewiseDataInterval(cutPoints))
        {
            
        }

        public PiecewiseIntervalPolynomialCurveElement(int index, PiecewiseDataInterval interval)
        {
            Debug.Assert(index > -1 && index < interval.SubIntervals.Count, "The index is out of range when initializing the BSplineBasisFunctionIntervalPolynomialCurve.");
            polynomialCurves = new List<NormalIntervalPolynomialCurveElement>();
            if (interval.SubIntervals[index].NullInterval) equalsToZero = true;
            for (int i = 0; i < interval.SubIntervals.Count; i++)
            {
                polynomialCurves.Add(new NormalIntervalPolynomialCurveElement(new DoubleExtension((!equalsToZero && i == index) ? 1 : 0), interval.SubIntervals[i]));
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

        public List<NormalIntervalPolynomialCurveElement> Curves
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

        public NormalIntervalPolynomialCurveElement this[int index]
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
        public PiecewiseIntervalPolynomialCurveElement MultiplyByLinear(DoubleExtension val1, DoubleExtension val0)
        {
            List<NormalIntervalPolynomialCurveElement> curve = new List<NormalIntervalPolynomialCurveElement>();
            NormalIntervalPolynomialCurveElement item = null;
            for (int i = 0; i < Curves.Count; i++)
            {
                item = Curves[i].MultiplyByLinear(val1, val0);
                curve.Add(item);
            }
            return new PiecewiseIntervalPolynomialCurveElement(curve, Interval);
        }

        public PiecewiseIntervalPolynomialCurveElement DivideByNumber(DoubleExtension val)
        {
            List<NormalIntervalPolynomialCurveElement> curve = new List<NormalIntervalPolynomialCurveElement>();
            NormalIntervalPolynomialCurveElement item = null;
            for (int i = 0; i < Curves.Count; i++)
            {
                item = Curves[i].DivideByNumber(val);
                curve.Add(item);
            }
            return new PiecewiseIntervalPolynomialCurveElement(curve, Interval);
        }
        #endregion

        #region Operator
        public static PiecewiseIntervalPolynomialCurveElement operator +(PiecewiseIntervalPolynomialCurveElement c1, PiecewiseIntervalPolynomialCurveElement c2)
        {
            if (c1.Interval.Equals(c2.Interval))
            {
                List<NormalIntervalPolynomialCurveElement> curves = new List<NormalIntervalPolynomialCurveElement>();
                for (int i = 0; i < c1.Curves.Count; i++)
                {
                    curves.Add(c1.Curves[i] + c2.Curves[i]);
                }
                return new PiecewiseIntervalPolynomialCurveElement(curves, c1.Interval);
            }
            throw new ArgumentException("The two BSplineBasisFunctionIntervalPolynomialCurve have different intervals.");
        }
        #endregion
    }
}
