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

using Util.Variable;

namespace CurveBase.CurveElement.IntervalPolynomialCurve
{
    public abstract class IntervalPolynomialCurve
    {
        protected DataInterval interval;
        protected int degree;

        #region Public.Interface
        public abstract DoubleExtension calculate(DoubleExtension doubleExtension);
        #endregion

        #region Property
        public int Degree
        {
            get
            {
                return degree;
            }
        }

        public DataInterval Interval
        {
            get
            {
                return interval;
            }
        }

        public DataPoint LastPoint
        {
            get
            {
                return new DataPoint(Interval.RightBorder.AccurateValue, calculate(Interval.RightBorder).AccurateValue);
            }
        }
        #endregion
    }
}
