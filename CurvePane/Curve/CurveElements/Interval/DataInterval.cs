using System;
using System.Collections.Generic;
using System.Text;

using CurvePane.Extension.Variable;

namespace CurvePane.Curve.CurveElements.Interval
{
    public class DataInterval : IEquatable<DataInterval>
    {
        private DoubleExtension leftBorder, rightBorder;
        private bool nullInterval;

        public static DataInterval nullDataInterval = new DataInterval();

        #region Constructor
        public DataInterval(int val1, int val2)
        {
            if (val1 == val2)
            {
                leftBorder = new DoubleExtension(val1);
                rightBorder = new DoubleExtension(val2);
                nullInterval = true;
            }
            else if (val1 < val2)
            {
                leftBorder = new DoubleExtension(val1);
                rightBorder = new DoubleExtension(val2);
                nullInterval = false;
            }
            else
            {
                leftBorder = new DoubleExtension(val2);
                rightBorder = new DoubleExtension(val1);
                nullInterval = false;
            }
        }
        
        public DataInterval(double val1, double val2)
        {
            if (val1 == val2)
            {
                leftBorder = new DoubleExtension(val1);
                rightBorder = new DoubleExtension(val2);
                nullInterval = true;
            }
            else if (val1 < val2)
            {
                leftBorder = new DoubleExtension(val1);
                rightBorder = new DoubleExtension(val2);
                nullInterval = false;
            }
            else
            {
                leftBorder = new DoubleExtension(val2);
                rightBorder = new DoubleExtension(val1);
                nullInterval = false;
            }
        }

        public DataInterval(DoubleExtension val1, DoubleExtension val2)
        {
            if (val1 == val2)
            {
                leftBorder = val1;
                rightBorder = val2;
                nullInterval = true;
            }
            else if (val1 < val2)
            {
                leftBorder = val1;
                rightBorder = val2;
                nullInterval = false;
            }
            else
            {
                leftBorder = val2;
                rightBorder = val1;
                nullInterval = false;
            }
        }

        private DataInterval()
        {
            leftBorder = null;
            rightBorder = null;
            nullInterval = true;
        }
        #endregion

        #region Property
        public bool NullInterval
        {
            get
            {
                return nullInterval;
            }
        }

        public DoubleExtension LeftBorder
        {
            get
            {
                return leftBorder;
            }
        }

        public DoubleExtension RightBorder
        {
            get
            {
                return rightBorder;
            }
        }
        #endregion

        #region Public.Interface
        public bool isBetweenBordersCloseInterval(DoubleExtension db)
        {
            return Math.Abs(pointsPosition(db)) < 2;
        }

        public bool isBetweenBordersOpenInterval(DoubleExtension db)
        {
            return pointsPosition(db) == 0;
        }

        public int pointsPosition(DoubleExtension db)
        {
            if (db < LeftBorder) return -2;
            else if (db == LeftBorder) return -1;
            else if (db > RightBorder) return 2;
            else if (db == RightBorder) return 1;
            else return 0;
        }
        #endregion

        #region IEquatable<DataInterval>
        public bool Equals(DataInterval other)
        {
            if ((leftBorder == null || rightBorder == null) || (other.LeftBorder == null || other.RightBorder == null))
            {
                if ((leftBorder == null || rightBorder == null) && (other.LeftBorder == null || other.RightBorder == null))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return leftBorder == other.LeftBorder && rightBorder == other.RightBorder;
            }
        }
        #endregion
    }
}
