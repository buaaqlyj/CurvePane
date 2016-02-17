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

using Util.Variable;
using Util.Variable.Interval;
using Util.Variable.PointList;

namespace CurveBase.CurveData.CurveParamData
{
    public class bSplineCurveParam : ICurveParam, ICurvePointList
    {
        protected int degree;
        protected PiecewiseDataInterval interval;
        protected NormalCurvePointList pointsList;

        #region Constructor
        public bSplineCurveParam(List<DataPoint> points, int degree, List<DoubleExtension> cutPoints)
        {
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
        #endregion

        #region ICurveParam Member
        public CurveType getCurveType()
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

        public bool IsReadOnly
        {
            get { return false; }
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
