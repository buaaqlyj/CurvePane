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

namespace CurveBase.CurveElements.IntervalCurve
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

        public double calculate(double doubleValue)
        {
            if (!interval.isBetweenBordersCloseInterval(new DoubleExtension(doubleValue)))
                throw new ArgumentOutOfRangeException("doubleValue", "The value given is out of borders of intervals. Value: " + doubleValue.ToString("0.000") + ", Range: [" + interval.LeftBorder.CoordinateString + ", " + interval.RightBorder.CoordinateString + "].");
            double result = 0;
            double poweredX = 1;
            for (int i = 0; i < degree; i++)
            {
                result += poweredX * coefficients[i];
                poweredX *= doubleValue;
            }
            return result;
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
                return new DataPoint(Interval.RightBorder.CoordinateValue, calculate(Interval.RightBorder.CoordinateValue));
            }
        }
        #endregion
    }
}