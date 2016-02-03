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
using System.Text;

namespace CurvePane.Extension.Variable
{
    public class DoubleExtension : IEquatable<DoubleExtension>
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
        public double CoordinateValue
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

        public string CoordinateString
        {
            get
            {
                return va.ToString("0.000");
            }
        }
        #endregion

        #region Public.Interface
        public static bool areTheSameCoordinate(DoubleExtension v1, DoubleExtension v2)
        {
            return v1 == v2;
        }
        #endregion

        #region Operator
        #region +
        public static DoubleExtension operator +(DoubleExtension v1, DoubleExtension v2)
        {
            return new DoubleExtension(v1.CoordinateValue + v2.CoordinateValue);
        }
        public static DoubleExtension operator +(double v1, DoubleExtension v2)
        {
            return new DoubleExtension(v1 + v2.CoordinateValue);
        }
        public static DoubleExtension operator +(DoubleExtension v1, double v2)
        {
            return new DoubleExtension(v1.CoordinateValue + v2);
        }
        #endregion
        #region -
        public static DoubleExtension operator -(DoubleExtension v1, DoubleExtension v2)
        {
            return new DoubleExtension(v1.CoordinateValue - v2.CoordinateValue);
        }
        public static DoubleExtension operator -(double v1, DoubleExtension v2)
        {
            return new DoubleExtension(v1 - v2.CoordinateValue);
        }
        public static DoubleExtension operator -(DoubleExtension v1, double v2)
        {
            return new DoubleExtension(v1.CoordinateValue - v2);
        }
        #endregion
        #region *
        public static DoubleExtension operator *(DoubleExtension v1, DoubleExtension v2)
        {
            return new DoubleExtension(v1.CoordinateValue * v2.CoordinateValue);
        }
        public static DoubleExtension operator *(double v1, DoubleExtension v2)
        {
            return new DoubleExtension(v1 * v2.CoordinateValue);
        }
        public static DoubleExtension operator *(DoubleExtension v1, double v2)
        {
            return new DoubleExtension(v1.CoordinateValue * v2);
        }
        #endregion
        #region /
        public static DoubleExtension operator /(DoubleExtension v1, DoubleExtension v2)
        {
            return new DoubleExtension(v1.CoordinateValue / v2.CoordinateValue);
        }
        public static DoubleExtension operator /(double v1, DoubleExtension v2)
        {
            return new DoubleExtension(v1 / v2.CoordinateValue);
        }
        public static DoubleExtension operator /(DoubleExtension v1, double v2)
        {
            return new DoubleExtension(v1.CoordinateValue / v2);
        }
        #endregion
        #region <
        public static bool operator <(DoubleExtension v1, DoubleExtension v2)
        {
            return v1.CoordinateValue < v2.CoordinateValue;
        }
        public static bool operator <(double v1, DoubleExtension v2)
        {
            return v1 < v2.CoordinateValue;
        }
        public static bool operator <(DoubleExtension v1, double v2)
        {
            return v1.CoordinateValue < v2;
        }
        #endregion
        #region >
        public static bool operator >(DoubleExtension v1, DoubleExtension v2)
        {
            return v1.CoordinateValue > v2.CoordinateValue;
        }
        public static bool operator >(double v1, DoubleExtension v2)
        {
            return v1 > v2.CoordinateValue;
        }
        public static bool operator >(DoubleExtension v1, double v2)
        {
            return v1.CoordinateValue > v2;
        }
        #endregion
        #region <=
        public static bool operator <=(DoubleExtension v1, DoubleExtension v2)
        {
            return v1.CoordinateValue <= v2.CoordinateValue;
        }
        public static bool operator <=(double v1, DoubleExtension v2)
        {
            return v1 <= v2.CoordinateValue;
        }
        public static bool operator <=(DoubleExtension v1, double v2)
        {
            return v1.CoordinateValue <= v2;
        }
        #endregion
        #region >=
        public static bool operator >=(DoubleExtension v1, DoubleExtension v2)
        {
            return v1.CoordinateValue >= v2.CoordinateValue;
        }
        public static bool operator >=(double v1, DoubleExtension v2)
        {
            return v1 >= v2.CoordinateValue;
        }
        public static bool operator >=(DoubleExtension v1, double v2)
        {
            return v1.CoordinateValue >= v2;
        }
        #endregion
        #region ==
        public static bool operator ==(DoubleExtension v1, DoubleExtension v2)
        {
            return v1.CoordinateString == v2.CoordinateString;
        }
        public static bool operator ==(double v1, DoubleExtension v2)
        {
            return v1.ToString("0.000") == v2.CoordinateString;
        }
        public static bool operator ==(DoubleExtension v1, double v2)
        {
            return v1.CoordinateString == v2.ToString("0.000");
        }
        #endregion
        #region !=
        public static bool operator !=(DoubleExtension v1, DoubleExtension v2)
        {
            return v1.CoordinateString != v2.CoordinateString;
        }
        public static bool operator !=(double v1, DoubleExtension v2)
        {
            return v1.ToString("0.000") != v2.CoordinateString;
        }
        public static bool operator !=(DoubleExtension v1, double v2)
        {
            return v1.CoordinateString != v2.ToString("0.000");
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
            return base.GetHashCode();
        }
        #endregion
    }
}