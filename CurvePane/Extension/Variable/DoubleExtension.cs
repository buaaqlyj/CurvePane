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