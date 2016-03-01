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

using Util.Variable;
using Util.Variable.Interval;
using Util.Variable.PointList;

namespace CurveBase.CurveData.CurveParam
{
    public class BSplineCurveParam : ICurveParam, ICurvePointList
    {
        protected int degree;
        protected PiecewiseDataInterval interval;
        protected NormalCurvePointList pointList;

        #region Constructor
        public BSplineCurveParam(List<DataPoint> points, int degree, List<DoubleExtension> cutPoints)
        {
            if (degree + points.Count + 1 != cutPoints.Count)
            {
                throw new ArgumentException("The count of the cut points is invalid!");
            }
            if (points.Count < 2)
            {
                throw new ArgumentException("At least two points are needed to finish the interpolation.");
            }
            this.degree = degree;
            this.interval = new PiecewiseDataInterval(cutPoints);
            this.pointList = new NormalCurvePointList(points);
        }
        #endregion

        #region Property
        public NormalCurvePointList PointList
        {
            get
            {
                return pointList;
            }
        }

        public PiecewiseDataInterval Interval
        {
            get
            {
                return interval;
            }
        }

        public int Degree
        {
            get
            {
                return degree;
            }
        }

        public int Multiplycity
        {
            get
            {
                return Interval.Multiplycity;
            }
        }
        #endregion

        #region ICurveParam Member
        public virtual CurveType getCurveType()
        {
            return CurveType.bsCurve;
        }
        #endregion

        #region ICurvePointList Member
        public int IndexOf(DataPoint item)
        {
            return pointList.IndexOf(item);
        }

        public void RemoveAt(int index)
        {
            pointList.RemoveAt(index);
        }

        public DataPoint this[int index]
        {
            get
            {
                return pointList[index];
            }
            set
            {
                pointList[index] = value;
            }
        }

        public void Add(DataPoint item)
        {
            pointList.Add(item);
        }

        public void Clear()
        {
            pointList.Clear();
        }

        public bool Contains(DataPoint item)
        {
            return pointList.Contains(item);
        }

        public void CopyTo(DataPoint[] array, int arrayIndex)
        {
            pointList.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return pointList.Count; }
        }

        public bool Remove(DataPoint item)
        {
            return pointList.Remove(item);
        }

        public IEnumerator<DataPoint> GetEnumerator()
        {
            return pointList.GetEnumerator();
        }

        public string Label
        {
            get
            {
                return pointList.Label;
            }
            set
            {
                pointList.Label = value;
            }
        }
        #endregion
    }
}
