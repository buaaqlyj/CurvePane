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
using System.Text;

namespace Util.Variable
{
    public class DataPoint
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

        #region Public.Interface
        public double distance(DataPoint pt)
        {
            return Math.Sqrt(Math.Pow((pt.X - this.X).CoordinateValue, 2) + Math.Pow((pt.Y - this.Y).CoordinateValue, 2));
        }
        #endregion

        #region Class.Interface
        public static bool hasSameX(DataPoint pt1, DataPoint pt2)
        {
            return pt1.X == pt2.X;
        }

        public static double distance(DataPoint pt1, DataPoint pt2)
        {
            return Math.Sqrt(Math.Pow((pt1.X - pt2.X).CoordinateValue, 2) + Math.Pow((pt1.Y - pt2.Y).CoordinateValue, 2));
        }
        #endregion
    }
}