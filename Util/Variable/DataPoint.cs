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
    public class DataPoint : IEquatable<DataPoint>
    {
        private DoubleExtension x;
        private DoubleExtension y;

        #region Constructor
        public DataPoint(double x, double y)
        {
            this.x = new DoubleExtension(x);
            this.y = new DoubleExtension(y);
        }

        public DataPoint(int x, int y)
        {
            this.x = new DoubleExtension(x);
            this.y = new DoubleExtension(y);
        }

        public DataPoint(DoubleExtension x, DoubleExtension y)
        {
            this.x = x;
            this.y = y;
        }        
        #endregion

        #region Property
        public DoubleExtension X
        {
            get
            {
                return x;
            }
        }
        public DoubleExtension Y
        {
            get
            {
                return y;
            }
        }
        #endregion

        #region Class.Member
        public static bool TryParse(string text, out DataPoint point)
        {
            string[] strings = text.Split(new char[]{' ', ','}, StringSplitOptions.RemoveEmptyEntries);
            if (strings.Length == 2)
            {
                double val1, val2;
                if (double.TryParse(strings[0], out val1) && double.TryParse(strings[1], out val2))
                {
                    point = new DataPoint(val1, val2);
                    return true;
                }
            }
            point = new DataPoint(0, 0);
            return false;
        }
        #endregion

        #region Object.Member
        public override string ToString()
        {
            return X.ApproximateString + "," + Y.ApproximateString;
        }
        #endregion

        #region Public.Interface
        public double distance(DataPoint pt)
        {
            return Math.Sqrt(Math.Pow((pt.X - this.X).AccurateValue, 2) + Math.Pow((pt.Y - this.Y).AccurateValue, 2));
        }
        #endregion

        #region Class.Interface
        public static bool hasSameX(DataPoint pt1, DataPoint pt2)
        {
            return pt1.X == pt2.X;
        }

        public static double distance(DataPoint pt1, DataPoint pt2)
        {
            return Math.Sqrt(Math.Pow((pt1.X - pt2.X).AccurateValue, 2) + Math.Pow((pt1.Y - pt2.Y).AccurateValue, 2));
        }
        #endregion

        #region IEquatable<DataPoint>
        public bool Equals(DataPoint other)
        {
            if (this.X.ApproximateString == other.X.ApproximateString && this.Y.ApproximateString == other.Y.ApproximateString)
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}
