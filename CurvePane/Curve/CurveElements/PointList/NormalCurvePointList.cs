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

using CurvePane.Curve.CurveElements.Point;

namespace CurvePane.Curve.CurveElements.PointList
{
    public class NormalCurvePointList : ICurvePointList
    {
        protected List<CurvePoint> points;

        #region Constructor
        public NormalCurvePointList(List<CurvePoint> points)
        {
            this.points = points;
        }

        public NormalCurvePointList()
        {
            points = new List<CurvePoint>();
        }
        #endregion

        #region ICurvePointList
        public int IndexOf(CurvePoint item)
        {
            return points.IndexOf(item);
        }

        public void RemoveAt(int index)
        {
            points.RemoveAt(index);
        }

        public CurvePoint this[int index]
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

        public void Add(CurvePoint item)
        {
            points.Add(item);
        }

        public void Clear()
        {
            points.Clear();
        }

        public bool Contains(CurvePoint item)
        {
            return points.Contains(item);
        }

        public void CopyTo(CurvePoint[] array, int arrayIndex)
        {
            points.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return points.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(CurvePoint item)
        {
            return points.Remove(item);
        }

        public IEnumerator<CurvePoint> GetEnumerator()
        {
            return points.GetEnumerator();
        }
        #endregion

        #region Public.Interface
        
        public void Insert(int index, CurvePoint item)
        {
            points.Insert(index, item);
        }

        #endregion
    }
}
