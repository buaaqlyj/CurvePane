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

namespace Util.Variable
{
    public class DoubleExtension : IEquatable<DoubleExtension>, IComparer<DoubleExtension>, IComparable<DoubleExtension>
    {
        private double va;

        #region Constructor
        public DoubleExtension(double va)
        {
            this.va = va;
        }

        public DoubleExtension(int va)
        {
            this.va = Convert.ToDouble(va);
        }
        #endregion

        #region Property
        public double AccurateValue
        {
            get
            {
                return va;
            }
            set
            {
                va = value;
            }
        }

        public double ApproximateValue
        {
            get
            {
                return double.Parse(ApproximateString);
            }
        }

        public string ApproximateString
        {
            get
            {
                return va.ToString("0.000");
            }
        }

        public string CleanString
        {
            get
            {
                string text = ApproximateString;
                while (text != "0" && (text.EndsWith(".") || (text.Contains(".") && text.EndsWith("0"))))
                {
                    text = text.Substring(0, text.Length - 1);
                }
                return text;
            }
        }

        public int IdentificationValue
        {
            get
            {
                return (int)(1000 * double.Parse(ApproximateString));
            }
        }

        public bool IsZero
        {
            get
            {
                return ApproximateString == "0.000";
            }
        }
        #endregion

        #region Public.Interface
        public bool EqualsToZero()
        {
            return ApproximateString == "0.000";
        }
        #endregion

        #region Class Member
        public static DoubleExtension Zero = new DoubleExtension(0);
        public static DoubleExtension PositiveOne = new DoubleExtension(1);
        public static DoubleExtension NegativeOne = new DoubleExtension(-1);

        public static bool areTheSameCoordinate(DoubleExtension v1, DoubleExtension v2)
        {
            return v1 == v2;
        }

        public static bool EqualsToZero(double val)
        {
            return (new DoubleExtension(val)).ApproximateString == "0.000";
        }
        #endregion

        #region Object.Member
        public override string ToString()
        {
            return CleanString;
        }
        #endregion

        #region Operator
        #region +
        public static DoubleExtension operator +(DoubleExtension v1, DoubleExtension v2)
        {
            return new DoubleExtension(v1.AccurateValue + v2.AccurateValue);
        }
        public static DoubleExtension operator +(double v1, DoubleExtension v2)
        {
            return new DoubleExtension(v1 + v2.AccurateValue);
        }
        public static DoubleExtension operator +(DoubleExtension v1, double v2)
        {
            return new DoubleExtension(v1.AccurateValue + v2);
        }
        #endregion
        #region -
        public static DoubleExtension operator -(DoubleExtension v1, DoubleExtension v2)
        {
            return new DoubleExtension(v1.AccurateValue - v2.AccurateValue);
        }
        public static DoubleExtension operator -(double v1, DoubleExtension v2)
        {
            return new DoubleExtension(v1 - v2.AccurateValue);
        }
        public static DoubleExtension operator -(DoubleExtension v1, double v2)
        {
            return new DoubleExtension(v1.AccurateValue - v2);
        }
        #endregion
        #region *
        public static DoubleExtension operator *(DoubleExtension v1, DoubleExtension v2)
        {
            return new DoubleExtension(v1.AccurateValue * v2.AccurateValue);
        }
        public static DoubleExtension operator *(double v1, DoubleExtension v2)
        {
            return new DoubleExtension(v1 * v2.AccurateValue);
        }
        public static DoubleExtension operator *(DoubleExtension v1, double v2)
        {
            return new DoubleExtension(v1.AccurateValue * v2);
        }
        #endregion
        #region /
        public static DoubleExtension operator /(DoubleExtension v1, DoubleExtension v2)
        {
            return new DoubleExtension(v1.AccurateValue / v2.AccurateValue);
        }
        public static DoubleExtension operator /(double v1, DoubleExtension v2)
        {
            return new DoubleExtension(v1 / v2.AccurateValue);
        }
        public static DoubleExtension operator /(DoubleExtension v1, double v2)
        {
            return new DoubleExtension(v1.AccurateValue / v2);
        }
        #endregion
        #region <
        public static bool operator <(DoubleExtension v1, DoubleExtension v2)
        {
            return v1.IdentificationValue < v2.IdentificationValue;
        }
        public static bool operator <(double v1, DoubleExtension v2)
        {
            return (int)(1000 * v1) < v2.IdentificationValue;
        }
        public static bool operator <(DoubleExtension v1, double v2)
        {
            return v1.IdentificationValue < (int)(1000 * v2);
        }
        #endregion
        #region >
        public static bool operator >(DoubleExtension v1, DoubleExtension v2)
        {
            return v1.IdentificationValue > v2.IdentificationValue;
        }
        public static bool operator >(double v1, DoubleExtension v2)
        {
            return (int)(1000 * v1) > v2.IdentificationValue;
        }
        public static bool operator >(DoubleExtension v1, double v2)
        {
            return v1.IdentificationValue > (int)(1000 * v2);
        }
        #endregion
        #region <=
        public static bool operator <=(DoubleExtension v1, DoubleExtension v2)
        {
            return v1.IdentificationValue <= v2.IdentificationValue;
        }
        public static bool operator <=(double v1, DoubleExtension v2)
        {
            return (int)(1000 * v1) <= v2.IdentificationValue;
        }
        public static bool operator <=(DoubleExtension v1, double v2)
        {
            return v1.IdentificationValue <= (int)(1000 * v2);
        }
        #endregion
        #region >=
        public static bool operator >=(DoubleExtension v1, DoubleExtension v2)
        {
            return v1.IdentificationValue >= v2.IdentificationValue;
        }
        public static bool operator >=(double v1, DoubleExtension v2)
        {
            return (int)(1000 * v1) >= v2.IdentificationValue;
        }
        public static bool operator >=(DoubleExtension v1, double v2)
        {
            return v1.IdentificationValue >= (int)(1000 * v2);
        }
        #endregion
        #region ==
        public static bool operator ==(DoubleExtension v1, DoubleExtension v2)
        {
            return v1.ApproximateString == v2.ApproximateString;
        }
        public static bool operator ==(double v1, DoubleExtension v2)
        {
            return v1.ToString("0.000") == v2.ApproximateString;
        }
        public static bool operator ==(DoubleExtension v1, double v2)
        {
            return v1.ApproximateString == v2.ToString("0.000");
        }
        #endregion
        #region !=
        public static bool operator !=(DoubleExtension v1, DoubleExtension v2)
        {
            return v1.ApproximateString != v2.ApproximateString;
        }
        public static bool operator !=(double v1, DoubleExtension v2)
        {
            return v1.ToString("0.000") != v2.ApproximateString;
        }
        public static bool operator !=(DoubleExtension v1, double v2)
        {
            return v1.ApproximateString != v2.ToString("0.000");
        }
        #endregion
        #endregion

        #region IEquatable<DoubleExtension> Member
        public bool Equals(DoubleExtension other)
        {
            if (this == other) return true;
            else return false;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(DoubleExtension))
                return Equals((DoubleExtension)obj);
            else return false;
        }

        public override int GetHashCode()
        {
            return ApproximateString.GetHashCode();
        }
        #endregion

        #region IComparer<DoubleExtension> Member
        public int Compare(DoubleExtension x, DoubleExtension y)
        {
            if (x > y) return 1;
            else if (x < y) return -1;
            else return 0;
        }
        #endregion

        #region IComparable<DoubleExtension> Member
        public int CompareTo(DoubleExtension other)
        {
            return Compare(this, other);
        }
        #endregion
    }
}