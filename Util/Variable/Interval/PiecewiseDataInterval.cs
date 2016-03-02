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

using Util.Tool;
using Util.Variable.PointList;

namespace Util.Variable.Interval
{
    public class PiecewiseDataInterval : DataInterval, IEquatable<DataInterval>
    {
        protected List<DoubleExtension> cutPoints;
        protected List<DataInterval> subIntervals;
        protected int multiplycity;

        #region Constructor
        public PiecewiseDataInterval(DoubleExtension val1, DoubleExtension val2, List<DoubleExtension> cutPoints)
            : base(val1, val2)
        {
            cutPoints.Add(val2);
            cutPoints.Insert(0, val1);
            this.cutPoints = cutPoints;
            if (nullInterval)
            {
                throw new ArgumentException("Null interval cannot have cut points.", "cutPoints");
            }
            if (cutPoints.Count > 0)
            {
                DoubleExtension lastOne = cutPoints[0];
                for (int i = 1; i < cutPoints.Count; i++)
                {
                    if (lastOne > cutPoints[i])
                        throw new ArgumentException("The cut points are not monotonic nondecreasing.", "cutPoints");
                    lastOne = cutPoints[i];
                }

                subIntervals = new List<DataInterval>();
                for (int i = 1; i < cutPoints.Count; i++)
                {
                    subIntervals.Add(new DataInterval(cutPoints[i - 1], cutPoints[i]));
                }
            }
            else
            {
                subIntervals = new List<DataInterval>();
                subIntervals.Add(this);
            }
            multiplycity = ArrayExtension.GetMaxCountFromArray<DoubleExtension>(cutPoints);
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
            if (nullInterval)
            {
                throw new ArgumentException("Null interval cannot have cut points.", "cutPoints");
            }
            if (cutPoints.Count > 0)
            {
                DoubleExtension lastOne = cutPoints[0];
                for (int i = 1; i < cutPoints.Count; i++)
                {
                    if (lastOne > cutPoints[i])
                        throw new ArgumentException("The cut points are not monotonic nondecreasing.", "cutPoints");
                    lastOne = cutPoints[i];
                }
                
                subIntervals = new List<DataInterval>();
                for (int i = 1; i < cutPoints.Count; i++)
                {
                    subIntervals.Add(new DataInterval(cutPoints[i - 1], cutPoints[i]));
                }
            }
            else
            {
                subIntervals = new List<DataInterval>();
                subIntervals.Add(this);
            }
            multiplycity = ArrayExtension.GetMaxCountFromArray<DoubleExtension>(cutPoints);
        }

        public PiecewiseDataInterval(ICurvePointList points)
            : this(points.XList)
        {
            
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

        public int Multiplycity
        {
            get
            {
                return multiplycity;
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
            if (isBetweenBordersCloseInterval(val))
            {
                for (int i = 0; i < SubIntervals.Count; i++)
                {
                    if (!SubIntervals[i].NullInterval && SubIntervals[i].isBetweenBordersCloseInterval(val))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
        #endregion

        #region IEquatable<DataInterval>
        public bool Equals(PiecewiseDataInterval other)
        {
            if (base.Equals(other))
            {
                if (other.CutPoints.Count == this.CutPoints.Count)
                {
                    for (int i = 0; i < this.CutPoints.Count; i++)
                    {
                        if (other.CutPoints[i] != this.CutPoints[i])
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}
