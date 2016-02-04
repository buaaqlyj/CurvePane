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

namespace Util.Variable
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
            leftBorder = new DoubleExtension(0);
            rightBorder = new DoubleExtension(0);
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

        public DoubleExtension Length
        {
            get
            {
                return rightBorder - leftBorder;
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
