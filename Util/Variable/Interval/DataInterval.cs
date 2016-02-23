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

namespace Util.Variable.Interval
{
    public class DataInterval : IEquatable<DataInterval>
    {
        protected DoubleExtension leftBorder, rightBorder;
        protected bool nullInterval;

        public static DataInterval nullDataInterval = new DataInterval();

        #region Constructor
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

        public DataInterval(int val1, int val2)
            : this(new DoubleExtension(val1), new DoubleExtension(val2))
        {
            
        }
        
        public DataInterval(double val1, double val2)
            : this(new DoubleExtension(val1), new DoubleExtension(val2))
        {
            
        }

        protected DataInterval()
            : this((int)0, (int)0)
        {
            
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

        public DoubleExtension Length
        {
            get
            {
                return rightBorder - leftBorder;
            }
        }
        #endregion

        #region Object.Member
        public override string ToString()
        {
            return "[" + LeftBorder.CleanString + ", " + RightBorder.CleanString + "]";
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

        #region Class.Interface
        public static DataInterval Intersection(DataInterval di1, DataInterval di2)
        {
            if (di1.RightBorder <= di2.LeftBorder || di1.LeftBorder >= di2.RightBorder)
            {
                return nullDataInterval;
            }
            else
            {
                return new DataInterval(Math.Max(di1.LeftBorder.AccurateValue, di2.LeftBorder.AccurateValue), Math.Min(di1.RightBorder.AccurateValue, di2.RightBorder.AccurateValue));
            }
        }
        #endregion

        #region IEquatable<DataInterval>
        public bool Equals(DataInterval other)
        {
            return leftBorder.Equals(other.LeftBorder) && rightBorder.Equals(other.RightBorder);
        }
        #endregion
    }
}
