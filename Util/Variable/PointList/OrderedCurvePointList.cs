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
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Util.Variable;

namespace Util.Variable.PointList
{
    public class OrderedCurvePointList : ICurvePointList
    {
        protected SortedList<DoubleExtension, DataPoint> sortedPointList;

        #region Constructor
        public OrderedCurvePointList(List<DataPoint> points)
        {
            sortedPointList = new SortedList<DoubleExtension, DataPoint>();
            foreach (DataPoint pt in points)
            {
                sortedPointList.Add(pt.X, pt);
            }
        }

        public OrderedCurvePointList()
        {
            sortedPointList = new SortedList<DoubleExtension, DataPoint>();
        }
        #endregion

        #region ICurvePointList
        public int IndexOf(DataPoint item)
        {
            return sortedPointList.IndexOfValue(item);
        }

        public void RemoveAt(int index)
        {
            sortedPointList.RemoveAt(index);
        }

        public DataPoint this[int index]
        {
            get
            {
                return sortedPointList.Values[index];
            }
            set
            {
                sortedPointList.Values[index] = value;
            }
        }

        public void Add(DataPoint item)
        {
            sortedPointList.Add(item.X, item);
        }

        public void Clear()
        {
            sortedPointList.Clear();
        }

        public bool Contains(DataPoint item)
        {
            return sortedPointList.Values.Contains(item);
        }

        public void CopyTo(DataPoint[] array, int arrayIndex)
        {
            sortedPointList.Values.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return sortedPointList.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(DataPoint item)
        {
            return sortedPointList.Remove(sortedPointList.Keys[sortedPointList.IndexOfValue(item)]);
        }

        public IEnumerator<DataPoint> GetEnumerator()
        {
            return sortedPointList.Values.GetEnumerator();
        }
        #endregion

        #region Public.Interface
        public int IndexOf(DoubleExtension item)
        {
            return sortedPointList.IndexOfKey(item);
        }

        public bool noDuplicatedX()
        {
            if (sortedPointList.Count < 2) return true;
            for (int i = 1; i < sortedPointList.Count; i++)
            {
                if (DataPoint.hasSameX(sortedPointList.Values[i - 1], sortedPointList.Values[i]))
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
    }
}
