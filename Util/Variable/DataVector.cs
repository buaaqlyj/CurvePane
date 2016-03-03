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

namespace Util.Variable
{
    public class DataVector
    {
        private DoubleExtension xDiff = DoubleExtension.ZERO, yDiff = DoubleExtension.ZERO;
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
                xDiff = DoubleExtension.ZERO;
                yDiff = DoubleExtension.POSITIVE_ONE;
            }
            else if (arc == Math.PI / (-2.0))
            {
                xDiff = DoubleExtension.ZERO;
                yDiff = DoubleExtension.NEGATIVE_ONE;
            }
            else if (positive)
            {
                xDiff = DoubleExtension.POSITIVE_ONE;
                yDiff = new DoubleExtension(Math.Tan(arc.AccurateValue));
            }
            else
            {
                xDiff = DoubleExtension.NEGATIVE_ONE;
                yDiff = new DoubleExtension(0 - Math.Tan(arc.AccurateValue));
            }
            isZero = false;
        }
        #endregion

        #region Property
        public bool EqualsToZero
        {
            get
            {
                return isZero;
            }
        }

        public DoubleExtension Arc
        {
            get
            {
                if (xDiff == DoubleExtension.ZERO)
                {
                    if (yDiff > 0) return new DoubleExtension(Math.PI / 2.0);
                    else if (yDiff < 0) return new DoubleExtension(Math.PI / -2.0);
                    else return DoubleExtension.ZERO;
                }
                return new DoubleExtension(Math.Atan2(yDiff.AccurateValue, xDiff.AccurateValue));
            }
        }

        public DoubleExtension Gradient
        {
            get
            {
                if (xDiff == DoubleExtension.ZERO)
                {
                    if (yDiff > 0) return new DoubleExtension(double.MaxValue);
                    else if (yDiff < 0) return new DoubleExtension(double.MinValue);
                    else return DoubleExtension.ZERO;
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
        public static DoubleExtension CalculateInnerProduct(DataVector dv1, DataVector dv2)
        {
            return dv1.X * dv2.X + dv1.Y * dv2.Y;
        }
        #endregion
    }
}
