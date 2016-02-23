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
        protected NormalCurvePointList pointsList;

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
            this.pointsList = new NormalCurvePointList(points);
        }
        #endregion

        #region Property
        public NormalCurvePointList PointList
        {
            get
            {
                return pointsList;
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
            return pointsList.IndexOf(item);
        }

        public void RemoveAt(int index)
        {
            pointsList.RemoveAt(index);
        }

        public DataPoint this[int index]
        {
            get
            {
                return pointsList[index];
            }
            set
            {
                pointsList[index] = value;
            }
        }

        public void Add(DataPoint item)
        {
            pointsList.Add(item);
        }

        public void Clear()
        {
            pointsList.Clear();
        }

        public bool Contains(DataPoint item)
        {
            return pointsList.Contains(item);
        }

        public void CopyTo(DataPoint[] array, int arrayIndex)
        {
            pointsList.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return pointsList.Count; }
        }

        public bool Remove(DataPoint item)
        {
            return pointsList.Remove(item);
        }

        public IEnumerator<DataPoint> GetEnumerator()
        {
            return pointsList.GetEnumerator();
        }

        public string Label
        {
            get
            {
                return pointsList.Label;
            }
            set
            {
                pointsList.Label = value;
            }
        }
        #endregion
    }
}
