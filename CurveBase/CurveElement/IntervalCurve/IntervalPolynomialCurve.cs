using System;
using System.Collections.Generic;
using System.Text;

using Util.Variable;

namespace CurveBase.CurveElement.IntervalCurve
{
    public abstract class IntervalPolynomialCurve
    {
        protected DataInterval interval;
        protected int degree;

        #region Public.Interface
        public abstract DoubleExtension calculate(DoubleExtension doubleExtension);

        public abstract double calculate(double doubleValue);
        #endregion

        #region Property
        public int Degree
        {
            get
            {
                return degree;
            }
        }

        public DataInterval Interval
        {
            get
            {
                return interval;
            }
        }

        public DataPoint LastPoint
        {
            get
            {
                return new DataPoint(Interval.RightBorder.AccurateValue, calculate(Interval.RightBorder.AccurateValue));
            }
        }
        #endregion
    }
}
