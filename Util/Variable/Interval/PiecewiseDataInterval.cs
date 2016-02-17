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
    public class PiecewiseDataInterval : DataInterval
    {
        protected List<DoubleExtension> cutPoints;
        protected List<DataInterval> subIntervals;

        #region Constructor
        public PiecewiseDataInterval(DoubleExtension val1, DoubleExtension val2, List<DoubleExtension> cutPoints)
            : base(val1, val2)
        {
            if (nullInterval)
            {
                throw new ArgumentException("Null interval cannot have cut points.", "cutPoints");
            }
            if (cutPoints.Count > 0)
            {
                DoubleExtension lastOne = leftBorder;
                foreach (DoubleExtension item in cutPoints)
                {
                    if (lastOne > item)
                        throw new ArgumentException("The cut points are not monotonic nondecreasing.", "cutPoints");
                    if (rightBorder < item)
                        throw new ArgumentException("Some of the cut points are exceed the right border.", "cutPoints");
                    lastOne = item;
                }
                subIntervals = new List<DataInterval>();
                subIntervals.Add(new DataInterval(leftBorder, cutPoints[0]));
                for (int i = 1; i < cutPoints.Count; i++)
                {
                    subIntervals.Add(new DataInterval(cutPoints[i - 1], cutPoints[i]));
                }
                subIntervals.Add(new DataInterval(cutPoints[cutPoints.Count - 1], rightBorder));
            }
            else
            {
                subIntervals = new List<DataInterval>();
                subIntervals.Add(this);
            }
            cutPoints.Add(val2);
            cutPoints.Insert(0, val1);
            this.cutPoints = cutPoints;
        }

        public PiecewiseDataInterval(int val1, int val2, List<DoubleExtension> cutPoints)
            : this(new DoubleExtension(val1), new DoubleExtension(val2), cutPoints)
        {
            
        }

        public PiecewiseDataInterval(double val1, double val2, List<DoubleExtension> cutPoints)
            : this(new DoubleExtension(val1), new DoubleExtension(val2), cutPoints)
        {
            
        }

        public PiecewiseDataInterval(List<DoubleExtension> cutPoints)
            : base(cutPoints[0], cutPoints[cutPoints.Count - 1])
        {
            this.cutPoints = cutPoints;
            cutPoints.RemoveAt(cutPoints.Count - 1);
            cutPoints.RemoveAt(0);
            if (nullInterval)
            {
                throw new ArgumentException("Null interval cannot have cut points.", "cutPoints");
            }
            if (cutPoints.Count > 0)
            {
                DoubleExtension lastOne = leftBorder;
                foreach (DoubleExtension item in cutPoints)
                {
                    if (lastOne > item)
                        throw new ArgumentException("The cut points are not monotonic nondecreasing.", "cutPoints");
                    if (rightBorder < item)
                        throw new ArgumentException("Some of the cut points are exceed the right border.", "cutPoints");
                    lastOne = item;
                }
                subIntervals = new List<DataInterval>();
                subIntervals.Add(new DataInterval(leftBorder, cutPoints[0]));
                for (int i = 1; i < cutPoints.Count; i++)
                {
                    subIntervals.Add(new DataInterval(cutPoints[i - 1], cutPoints[i]));
                }
                subIntervals.Add(new DataInterval(cutPoints[cutPoints.Count - 1], rightBorder));
            }
            else
            {
                subIntervals = new List<DataInterval>();
                subIntervals.Add(this);
            }
        }
        #endregion

        #region Property
        public List<DataInterval> SubIntervals
        {
            get
            {
                return subIntervals;
            }
        }

        public List<DoubleExtension> CutPoints
        {
            get
            {
                return cutPoints;
            }
        }

        public DataInterval this[int index]
        {
            get
            {
                if (index < 0 || index >= subIntervals.Count)
                {
                    throw new ArgumentOutOfRangeException("index", "The index is out of range.");
                }
                else
                {
                    return subIntervals[index];
                }
            }
        }
        #endregion

        #region Public.Interface
        /// <summary>
        /// 每个点属于它右边的区间
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public int findIntervalIndex(DoubleExtension val)
        {
            if (isBetweenBordersOpenInterval(val))
            {
                for (int i = 0; i < cutPoints.Count - 1; i++)
                {
                    if (val < cutPoints[i + 1])
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
        #endregion
    }
}
