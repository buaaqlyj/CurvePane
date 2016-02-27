﻿/// Copyright 2016 Troy Lewis. Some Rights Reserved
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
    public class LagarangeIntervalPolynomialCurveElement : IntervalPolynomialCurveElement
    {
        protected List<DoubleExtension> coefficients;
        protected bool equalsToZero = false;

        public static LagarangeIntervalPolynomialCurveElement nullLagarangePolynomialCurve = new LagarangeIntervalPolynomialCurveElement();

        #region Constructor
        public LagarangeIntervalPolynomialCurveElement(List<DoubleExtension> coefficients, int degree, DataInterval interval)
        {
            if (degree >= 0)
            {
                this.degree = degree;
                this.coefficients = new List<DoubleExtension>();
                for (int i = 0; i < degree; i++ )
                    this.coefficients.Add(coefficients[i]);
            }
            if (this.coefficients.Count == 1 && this.coefficients[0].EqualsToZero())
                equalsToZero = true;
            this.interval = interval;
        }

        public LagarangeIntervalPolynomialCurveElement(List<DoubleExtension> coefficients, int degree, DoubleExtension borderVal1, DoubleExtension borderVal2)
            : this(coefficients, degree, new DataInterval(borderVal1, borderVal2))
        {
            
        }

        public LagarangeIntervalPolynomialCurveElement(DoubleExtension val, DataInterval interval)
        {
            this.degree = 1;
            this.coefficients = new List<DoubleExtension>();
            this.coefficients.Add(val);
            this.interval = interval;
            if (val.EqualsToZero())
                this.equalsToZero = true;
        }

        protected LagarangeIntervalPolynomialCurveElement()
            : this(new DoubleExtension(0), DataInterval.nullDataInterval)
        {
            
        }
        #endregion

        #region Property
        public bool EqualsToZero
        {
            get
            {
                return equalsToZero;
            }
        }
        #endregion

        #region Public.Interface
        public LagarangeIntervalPolynomialCurveElement MultiplyByLinear(DoubleExtension val1, DoubleExtension val0)
        {
            if (EqualsToZero)
            {
                return new LagarangeIntervalPolynomialCurveElement(new DoubleExtension(0), Interval);
            }
            List<DoubleExtension> newCoefficients = new List<DoubleExtension>();
            for (int i = 0; i < degree; i++)
            {
                newCoefficients.Add(coefficients[i] * val0);
            }
            if (!(coefficients[degree - 1] * val1).EqualsToZero())
            {
                newCoefficients.Add(coefficients[degree - 1] * val1);
            }
            if (degree > 1)
            {
                for (int i = 1; i < degree; i++)
                {
                    newCoefficients[i] += coefficients[i - 1] * val1;
                }
            }
            return new LagarangeIntervalPolynomialCurveElement(newCoefficients, newCoefficients.Count, Interval);
        }

        public LagarangeIntervalPolynomialCurveElement DivideByNumber(DoubleExtension val)
        {
            if (EqualsToZero)
            {
                return new LagarangeIntervalPolynomialCurveElement(new DoubleExtension(0), Interval);
            }
            if (val.EqualsToZero())
            {
                throw new DivideByZeroException("The polynomial coefficients are divided by 0.");
            }
            
            List<DoubleExtension> newCoefficients = new List<DoubleExtension>();
            for (int i = 0; i < coefficients.Count; i++)
            {
                newCoefficients.Add(coefficients[i] / val);
            }
            return new LagarangeIntervalPolynomialCurveElement(newCoefficients, newCoefficients.Count, Interval);
        }
        #endregion

        #region Object Member
        public override string ToString()
        {
            bool notFirstTime = false;
            if (coefficients.Count > 0)
            {
                string result = "f(x) = ";
                string cleanCoe = "";
                string itemString = "";
                //从高次到低次
                for (int i = coefficients.Count - 1; i > -1; i--)
                {
                    cleanCoe = coefficients[i].CleanString;
                    if (i == 0)
                    {
                        //遇到常数项
                        if (!notFirstTime)
                        {
                            //等号右边仍为空，就原样写，有0也写0
                            result += cleanCoe;
                            //右边写入常数项以后变为不空
                            notFirstTime = true;
                        }
                        else if (cleanCoe != "0")
                        {
                            if (!cleanCoe.StartsWith("-"))
                            {
                                //当系数为正且右边不空时要加上连接的加号
                                result += " +";
                            }
                            //等号右边已有其他内容，但该常数项非0，也原样写
                            result += cleanCoe;
                        }
                    }
                    else
                    {
                        //遇到非常数项，1可以省略系数，0可以在右边不空的时候省略整项
                        if (cleanCoe != "0")
                        {
                            //系数非0，该项不能省
                            itemString = "";
                            if (cleanCoe == "-1")
                            {
                                //系数为-1，省略掉1
                                itemString = " -";
                            }
                            else if (cleanCoe != "1")
                            {
                                //系数非正负1，不能省系数
                                itemString = " " + cleanCoe;
                            }
                            if (i == 1)
                            {
                                //次数为1，可以直接写成x
                                itemString += " x";
                            }
                            else
                            {
                                //次数大于1，要写明次数
                                itemString += " x^" + i.ToString();
                            }
                            if (!cleanCoe.StartsWith("-") && notFirstTime)
                            {
                                //当系数为正且右边不空时要加上连接的加号
                                itemString = " +" + itemString;
                            }
                            //把该项加入右边
                            result += itemString;
                            //右边写入一项以后变为不空
                            notFirstTime = true;
                        }
                    }
                }
                return result;
            }
            else
            {
                return "f(x) = N/A";
            }
        }
        #endregion

        #region IntervalPolynomialCurve Member
        public override DoubleExtension calculate(DoubleExtension doubleExtension)
        {
            if (!interval.isBetweenBordersCloseInterval(doubleExtension))
                throw new ArgumentOutOfRangeException("doubleExtension", "The value given is out of borders of intervals. Value: " + doubleExtension.ApproximateString + ", Range: [" + interval.LeftBorder.ApproximateString + ", " + interval.RightBorder.ApproximateString + "].");
            DoubleExtension result = new DoubleExtension(0);
            DoubleExtension poweredX = new DoubleExtension(1);
            for (int i = 0; i < degree; i++)
            {
                result += poweredX * coefficients[i];
                poweredX *= doubleExtension.AccurateValue;
            }
            return result;
        }
        #endregion

        #region Operator
        public static LagarangeIntervalPolynomialCurveElement operator +(LagarangeIntervalPolynomialCurveElement c1, LagarangeIntervalPolynomialCurveElement c2)
        {
            if (c1.equalsToZero) return c2;
            if (c2.equalsToZero) return c1;
            int degree = Math.Max(c1.Degree, c2.Degree);
            int i = 0;
            DoubleExtension val1, val2;
            List<DoubleExtension> coefficients = new List<DoubleExtension>();
            while (i < degree)
            {
                if (i < c1.Degree)
                {
                    val1 = c1.coefficients[i];
                }
                else
                {
                    val1 = new DoubleExtension(0);
                }
                if (i < c2.Degree)
                {
                    val2 = c2.coefficients[i];
                }
                else
                {
                    val2 = new DoubleExtension(0);
                }
                coefficients.Add(val1 + val2);
                i++;
            }
            return new LagarangeIntervalPolynomialCurveElement(coefficients, degree, DataInterval.Intersection(c1.Interval, c2.Interval));
        }
        
        public static LagarangeIntervalPolynomialCurveElement operator -(LagarangeIntervalPolynomialCurveElement c1, LagarangeIntervalPolynomialCurveElement c2)
        {
            if (c2.equalsToZero) return c1;
            int degree = Math.Max(c1.Degree, c2.Degree);
            int i = 0;
            DoubleExtension val1, val2;
            List<DoubleExtension> coefficients = new List<DoubleExtension>();
            while (i++ < degree)
            {
                if (i < c1.Degree)
                {
                    val1 = c1.coefficients[i];
                }
                else
                {
                    val1 = new DoubleExtension(0);
                }
                if (i < c2.Degree)
                {
                    val2 = c2.coefficients[i];
                }
                else
                {
                    val2 = new DoubleExtension(0);
                }
                coefficients.Add(val1 - val2);
            }
            return new LagarangeIntervalPolynomialCurveElement(coefficients, degree, DataInterval.Intersection(c1.Interval, c2.Interval));
        }

        public static LagarangeIntervalPolynomialCurveElement operator *(DoubleExtension c1, LagarangeIntervalPolynomialCurveElement c2)
        {
            if (c1.EqualsToZero()) return new LagarangeIntervalPolynomialCurveElement(c1, c2.Interval);
            for (int i = 0; i < c2.coefficients.Count; i++ )
            {
                c2.coefficients[i] *= c1;
            }
            return c2;
        }
        
        public static LagarangeIntervalPolynomialCurveElement operator *(LagarangeIntervalPolynomialCurveElement c1, LagarangeIntervalPolynomialCurveElement c2)
        {
            int degree = c1.Degree + c2.Degree - 1;
            List<DoubleExtension> coefficients = new List<DoubleExtension>();
            for (int i = 0; i < degree; i++)
            {
                if (i < c2.Degree)
                {
                    coefficients.Add(c1.coefficients[0] * c2.coefficients[i]);
                }
                else
                {
                    coefficients.Add(new DoubleExtension(0));
                }
            }
            for (int i = 1; i < c1.Degree; i++)
            {
                for (int j = 0; j < c2.Degree; j++)
                {
                    coefficients[i + j] += c1.coefficients[i] * c2.coefficients[j];
                }
            }
            return new LagarangeIntervalPolynomialCurveElement(coefficients, degree, DataInterval.Intersection(c1.Interval, c2.Interval));
        }
        #endregion
    }
}