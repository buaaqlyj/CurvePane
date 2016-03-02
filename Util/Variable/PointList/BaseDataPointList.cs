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

using System.Collections.Generic;

using Util.Enum;

namespace Util.Variable.PointList
{
    public class BaseDataPointList : ICurvePointList
    {
        protected List<DataPoint> points;
        protected SortedList<DoubleExtension, DataPoint> sortedPointList;
        protected string label = "";
        protected PaneCurveType paneCurveType = PaneCurveType.baseCurve;

        #region Constructor
        public BaseDataPointList()
        {
            points = new List<DataPoint>();
            sortedPointList = new SortedList<DoubleExtension, DataPoint>();
        }

        public BaseDataPointList(List<DataPoint> points, SortedList<DoubleExtension, DataPoint> sortedPointList)
        {
            this.points = points;
            this.sortedPointList = sortedPointList;
        }
        #endregion

        #region ICurvePointList
        public int IndexOf(DataPoint item)
        {
            return points.IndexOf(item);
        }

        public void RemoveAt(int index)
        {
            sortedPointList.Remove(points[index].X);
            points.RemoveAt(index);
        }

        public bool Contains(DataPoint item)
        {
            return points.Contains(item);
        }

        public int Count
        {
            get { return points.Count; }
        }

        public DataPoint this[int index]
        {
            get
            {
                return points[index];
            }
            set
            {
                points[index] = value;
            }
        }

        public void Add(DataPoint item)
        {
            if (!sortedPointList.Keys.Contains(item.X))
            {
                points.Add(item);
                sortedPointList.Add(item.X, item);
            }
        }

        public void Clear()
        {
            points.Clear();
            sortedPointList.Clear();
        }

        public void CopyTo(DataPoint[] array, int arrayIndex)
        {
            points.CopyTo(array, arrayIndex);
        }

        public bool Remove(DataPoint item)
        {
            bool result1 = sortedPointList.Remove(item.X);
            bool result2 = points.Remove(item);
            return result1 && result2;
        }

        public IEnumerator<DataPoint> GetEnumerator()
        {
            return points.GetEnumerator();
        }

        public string Label
        {
            get
            {
                return label;
            }
            set
            {
                label = value;
            }
        }

        public PaneCurveType PaneCurveType
        {
            get
            {
                return paneCurveType;
            }
            set
            {
                paneCurveType = value;
            }
        }

        public List<DoubleExtension> XList
        {
            get
            {
                List<DoubleExtension> X = new List<DoubleExtension>();
                foreach (DataPoint item in points)
                {
                    X.Add(item.X);
                }
                return X;
            }
        }

        public List<DoubleExtension> YList
        {
            get
            {
                List<DoubleExtension> Y = new List<DoubleExtension>();
                foreach (DataPoint item in points)
                {
                    Y.Add(item.Y);
                }
                return Y;
            }
        }
        #endregion

        #region Property
        public List<DataPoint> Points
        {
            get
            {
                return points;
            }
        }

        public List<DataPoint> SortedPointList
        {
            get
            {
                List<DataPoint> list = new List<DataPoint>();
                list.AddRange(sortedPointList.Values);
                return list;
            }
        }
        #endregion

        #region Public.Interface
        public int SortedIndexOf(DoubleExtension item)
        {
            return sortedPointList.IndexOfKey(item);
        }

        public int SortedIndexOf(DataPoint point)
        {
            return sortedPointList.IndexOfValue(point);
        }
        
        public void OrderedCopyTo(DataPoint[] array, int arrayIndex)
        {
            sortedPointList.Values.CopyTo(array, arrayIndex);
        }

        public IEnumerator<DataPoint> GetOrderedEnumerator()
        {
            return sortedPointList.Values.GetEnumerator();
        }
        #endregion
    }
}
