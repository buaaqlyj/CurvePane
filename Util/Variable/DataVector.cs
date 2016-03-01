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
    public class DataVector
    {
        private DoubleExtension xDiff = DoubleExtension.Zero, yDiff = DoubleExtension.Zero;
        private bool isZero = true;

        #region Constructor
        private DataVector()
        {
            
        }

        public DataVector(DoubleExtension val1, DoubleExtension val2)
        {
            xDiff = val1;
            yDiff = val2;
            if (xDiff != 0 || yDiff != 0)
            {
                isZero = false;
            }
        }

        public DataVector(DataPoint pt1, DataPoint pt2)
        {
            xDiff = pt2.X - pt1.X;
            yDiff = pt2.Y - pt1.Y;
            if (xDiff != 0 || yDiff != 0)
            {
                isZero = false;
            }
        }

        public DataVector(DoubleExtension arc, bool positive)
        {
            if (arc == Math.PI / 2.0)
            {
                xDiff = DoubleExtension.Zero;
                yDiff = DoubleExtension.PositiveOne;
            }
            else if (arc == Math.PI / (-2.0))
            {
                xDiff = DoubleExtension.Zero;
                yDiff = DoubleExtension.NegativeOne;
            }
            else if (positive)
            {
                xDiff = DoubleExtension.PositiveOne;
                yDiff = new DoubleExtension(Math.Tan(arc.AccurateValue));
            }
            else
            {
                xDiff = DoubleExtension.NegativeOne;
                yDiff = new DoubleExtension(0 - Math.Tan(arc.AccurateValue));
            }
            isZero = false;
        }
        #endregion

        #region Property
        public bool IsZero
        {
            get
            {
                return isZero;
            }
        }

        public int Direction
        {
            get
            {
                if (xDiff > 0)
                {
                    if (yDiff > 0)
                    {
                        return 1;
                    }
                    else if (yDiff < 0)
                    {
                        return 4;
                    }
                    else
                    {
                        return 5;
                    }
                }
                else if (xDiff < 0)
                {
                    if (yDiff > 0)
                    {
                        return 2;
                    }
                    else if (yDiff < 0)
                    {
                        return 3;
                    }
                    else
                    {
                        return 7;
                    }
                }
                else
                {
                    if (yDiff > 0)
                    {
                        return 6;
                    }
                    else if (yDiff < 0)
                    {
                        return 8;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }

        public DoubleExtension Arc
        {
            get
            {
                if (xDiff == DoubleExtension.Zero)
                {
                    if (yDiff > 0) return new DoubleExtension(Math.PI / 2.0);
                    else if (yDiff < 0) return new DoubleExtension(Math.PI / -2.0);
                    else return DoubleExtension.Zero;
                }
                return new DoubleExtension(Math.Atan2(yDiff.AccurateValue, xDiff.AccurateValue));
            }
        }

        public DoubleExtension Gradient
        {
            get
            {
                if (xDiff == DoubleExtension.Zero)
                {
                    if (yDiff > 0) return new DoubleExtension(double.MaxValue);
                    else if (yDiff < 0) return new DoubleExtension(double.MinValue);
                    else return DoubleExtension.Zero;
                }
                return new DoubleExtension(Math.Atan2(yDiff.AccurateValue, xDiff.AccurateValue));
            }
        }

        public DoubleExtension X
        {
            get
            {
                return xDiff;
            }
        }

        public DoubleExtension Y
        {
            get
            {
                return yDiff;
            }
        }

        public DoubleExtension Length
        {
            get
            {
                return new DoubleExtension(Math.Sqrt((X * X + Y * Y).AccurateValue));
            }
        }
        #endregion

        #region Class Interface
        public static DoubleExtension InnerProduct(DataVector dv1, DataVector dv2)
        {
            return dv1.X * dv2.X + dv1.Y * dv2.Y;
        }

        public static DoubleExtension IncludedAngle(DataVector dv1, DataVector dv2)
        {
            return new DoubleExtension(Math.Acos((InnerProduct(dv1, dv2) / dv1.Length / dv2.Length).AccurateValue));
        }
        #endregion

        #region Public Interface
        public bool GetTheFlagForCloserArc(DoubleExtension arc)
        {
            DataVector pos = new DataVector(arc, true);
            DataVector neg = new DataVector(arc, false);
            DoubleExtension posVal = DataVector.InnerProduct(this, pos) / pos.Length;
            DoubleExtension negVal = DataVector.InnerProduct(this, neg) / neg.Length;
            if (posVal.AccurateValue >= negVal.AccurateValue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
