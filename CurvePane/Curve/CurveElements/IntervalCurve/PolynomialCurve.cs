using System;
using System.Collections.Generic;
using System.Text;

using CurvePane.Extension.Variable;
using CurvePane.Curve.CurveElements.Interval;

namespace CurvePane.Curve.CurveElements.IntervalCurve
{
    public class PolynomialCurve
    {
        protected DataInterval interval;
        protected List<double> coefficients;
        protected int degree;

        protected static PolynomialCurve nullPolynomialCurve = new PolynomialCurve();

        #region Constructor
        public PolynomialCurve(List<double> coefficients, int degree, DoubleExtension borderVal1, DoubleExtension borderVal2)
        {
            if (degree >= 0)
            {
                this.degree = degree;
                this.coefficients = new List<double>();
                for (int i = 0; i < degree; i++ )
                    this.coefficients.Add(coefficients[i]);
            }
            this.interval = new DataInterval(borderVal1, borderVal2);
        }

        private PolynomialCurve()
        {
            this.degree = 0;
            this.coefficients = new List<double>();
            this.coefficients.Add(0);
            this.interval = DataInterval.nullDataInterval;
        }
        #endregion

        #region Public.Interface
        public DoubleExtension calculate(DoubleExtension doubleExtension)
        {
            if (!interval.isBetweenBordersCloseInterval(doubleExtension))
                throw new ArgumentOutOfRangeException("doubleExtension", "The value given is out of borders of intervals. Value: " + doubleExtension.CoordinateString + ", Range: [" + interval.LeftBorder.CoordinateString + ", " + interval.RightBorder.CoordinateString + "].");
            double result = 0;
            double poweredX = 1;
            for (int i = 0; i < degree; i++)
            {
                result += poweredX * coefficients[i];
                poweredX *= doubleExtension.CoordinateValue;
            }
            return new DoubleExtension(result);
        }
        #endregion

        #region Property
        public int Degree
        {
            get
            {
                return degree;
            }
        }
        #endregion
    }
}