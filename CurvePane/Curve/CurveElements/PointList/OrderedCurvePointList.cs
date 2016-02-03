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

using CurvePane.Extension.Variable;
using CurvePane.Curve.CurveElements.Point;

namespace CurvePane.Curve.CurveElements.PointList
{
    public class OrderedCurvePointList : ICurvePointList
    {
        private SortedList<DoubleExtension, CurvePoint> sortedPointList;

        #region Constructor
        public OrderedCurvePointList(List<CurvePoint> points)
        {
            sortedPointList = new SortedList<DoubleExtension, CurvePoint>();
            foreach (CurvePoint pt in points)
            {
                sortedPointList.Add(pt.X, pt);
            }
        }

        public OrderedCurvePointList()
        {
            sortedPointList = new SortedList<DoubleExtension, CurvePoint>();
        }
        #endregion

        #region ICurvePointList
        public int IndexOf(CurvePoint item)
        {
            return sortedPointList.IndexOfValue(item);
        }

        public void RemoveAt(int index)
        {
            sortedPointList.RemoveAt(index);
        }

        public CurvePoint this[int index]
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

        public void Add(CurvePoint item)
        {
            sortedPointList.Add(item.X, item);
        }

        public void Clear()
        {
            sortedPointList.Clear();
        }

        public bool Contains(CurvePoint item)
        {
            return sortedPointList.Values.Contains(item);
        }

        public void CopyTo(CurvePoint[] array, int arrayIndex)
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

        public bool Remove(CurvePoint item)
        {
            return sortedPointList.Remove(sortedPointList.Keys[sortedPointList.IndexOfValue(item)]);
        }

        public IEnumerator<CurvePoint> GetEnumerator()
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
                if (CurvePoint.hasSameX(sortedPointList.Values[i - 1], sortedPointList.Values[i]))
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
    }
}
