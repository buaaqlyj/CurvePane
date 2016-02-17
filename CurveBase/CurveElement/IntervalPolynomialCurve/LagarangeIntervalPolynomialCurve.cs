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

using Util.Variable;
using Util.Variable.Interval;

namespace CurveBase.CurveElement.IntervalPolynomialCurve
{
    /// <summary>
    /// Polynomial Curve:
    /// P(x) = a[0] * x^0 + a[1] * x^1 + a[2] * x^2 + a[3] * x^3 + a[4] * x^4
    /// </summary>
    public class LagarangeIntervalPolynomialCurve : IntervalPolynomialCurve
    {
        protected List<double> coefficients;

        public static LagarangeIntervalPolynomialCurve nullLagarangePolynomialCurve = new LagarangeIntervalPolynomialCurve();

        #region Constructor
        public LagarangeIntervalPolynomialCurve(List<double> coefficients, int degree, DataInterval interval)
        {
            if (degree >= 0)
            {
                this.degree = degree;
                this.coefficients = new List<double>();
                for (int i = 0; i < degree; i++ )
                    this.coefficients.Add(coefficients[i]);
            }
            this.interval = interval;
        }

        public LagarangeIntervalPolynomialCurve(List<double> coefficients, int degree, DoubleExtension borderVal1, DoubleExtension borderVal2)
            : this(coefficients, degree, new DataInterval(borderVal1, borderVal2))
        {
            
        }

        public LagarangeIntervalPolynomialCurve(double val, DataInterval interval)
        {
            this.degree = 1;
            this.coefficients = new List<double>();
            this.coefficients.Add(val);
            this.interval = interval;
        }

        protected LagarangeIntervalPolynomialCurve()
            : this(0, DataInterval.nullDataInterval)
        {
            
        }
        #endregion

        #region IntervalPolynomialCurve Member
        public override DoubleExtension calculate(DoubleExtension doubleExtension)
        {
            if (!interval.isBetweenBordersCloseInterval(doubleExtension))
                throw new ArgumentOutOfRangeException("doubleExtension", "The value given is out of borders of intervals. Value: " + doubleExtension.ApproximateString + ", Range: [" + interval.LeftBorder.ApproximateString + ", " + interval.RightBorder.ApproximateString + "].");
            double result = 0;
            double poweredX = 1;
            for (int i = 0; i < degree; i++)
            {
                result += poweredX * coefficients[i];
                poweredX *= doubleExtension.AccurateValue;
            }
            return new DoubleExtension(result);
        }
        #endregion

        #region Operator
        public static LagarangeIntervalPolynomialCurve operator +(LagarangeIntervalPolynomialCurve c1, LagarangeIntervalPolynomialCurve c2)
        {
            int degree = Math.Max(c1.Degree, c2.Degree);
            int i = 0;
            double val1, val2;
            List<double> coefficients = new List<double>();
            while (i++ < degree)
            {
                if (i < c1.Degree)
                {
                    val1 = c1.coefficients[i];
                }
                else
                {
                    val1 = 0;
                }
                if (i < c2.Degree)
                {
                    val2 = c2.coefficients[i];
                }
                else
                {
                    val2 = 0;
                }
                coefficients.Add(val1 + val2);
            }
            return new LagarangeIntervalPolynomialCurve(coefficients, degree, DataInterval.Intersection(c1.Interval, c2.Interval));
        }
        
        public static LagarangeIntervalPolynomialCurve operator -(LagarangeIntervalPolynomialCurve c1, LagarangeIntervalPolynomialCurve c2)
        {
            int degree = Math.Max(c1.Degree, c2.Degree);
            int i = 0;
            double val1, val2;
            List<double> coefficients = new List<double>();
            while (i++ < degree)
            {
                if (i < c1.Degree)
                {
                    val1 = c1.coefficients[i];
                }
                else
                {
                    val1 = 0;
                }
                if (i < c2.Degree)
                {
                    val2 = c2.coefficients[i];
                }
                else
                {
                    val2 = 0;
                }
                coefficients.Add(val1 - val2);
            }
            return new LagarangeIntervalPolynomialCurve(coefficients, degree, DataInterval.Intersection(c1.Interval, c2.Interval));
        }

        public static LagarangeIntervalPolynomialCurve operator *(double c1, LagarangeIntervalPolynomialCurve c2)
        {
            for (int i = 0; i < c2.coefficients.Count; i++ )
            {
                c2.coefficients[i] *= c1;
            }
            return c2;
        }
        
        public static LagarangeIntervalPolynomialCurve operator *(LagarangeIntervalPolynomialCurve c1, LagarangeIntervalPolynomialCurve c2)
        {
            int degree = c1.Degree + c2.Degree - 1;
            List<double> coefficients = new List<double>();
            for (int i = 0; i < degree; i++)
            {
                if (i < c2.Degree)
                {
                    coefficients.Add(c1.coefficients[0] * c2.coefficients[i]);
                }
                else
                {
                    coefficients.Add(0);
                }
            }
            for (int i = 1; i < c1.Degree; i++)
            {
                for (int j = 0; j < c2.Degree; j++)
                {
                    coefficients[i + j] += c1.coefficients[i] * c2.coefficients[j];
                }
            }
            return new LagarangeIntervalPolynomialCurve(coefficients, degree, DataInterval.Intersection(c1.Interval, c2.Interval));
        }
        #endregion
    }
}