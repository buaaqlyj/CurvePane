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
using System.Collections;

using CurvePane.Curve.CurveElements.Point;
using CurvePane.Curve.CurveElements.PointList;

namespace CurvePane.Curve.CurveParam
{
    public class polynomialCurveParam : ICurveParam, ICurvePointList
    {
        private OrderedCurvePointList pointsList;
        private polynomialCurveType curveType;

        #region Constructor
        public polynomialCurveParam(List<CurvePoint> points, polynomialCurveType curveType)
        {
            pointsList = new OrderedCurvePointList(points);
            this.curveType = curveType;
        }
        #endregion

        #region Property
        public OrderedCurvePointList PointList
        {
            get
            {
                return pointsList;
            }
        }

        public polynomialCurveType PolynomialCurveType
        {
            get
            {
                return curveType;
            }
        }
        #endregion

        #region ICurveParam Member
        public CurveType getCurveType()
        {
            return CurveType.polynomialCurve;
        }
        #endregion

        #region ICurvePointList Member
        public int IndexOf(CurvePoint item)
        {
            return pointsList.IndexOf(item);
        }

        public void RemoveAt(int index)
        {
            pointsList.RemoveAt(index);
        }

        public CurvePoint this[int index]
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

        public void Add(CurvePoint item)
        {
            pointsList.Add(item);
        }

        public void Clear()
        {
            pointsList.Clear();
        }

        public bool Contains(CurvePoint item)
        {
            return pointsList.Contains(item);
        }

        public void CopyTo(CurvePoint[] array, int arrayIndex)
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

        public bool Remove(CurvePoint item)
        {
            return pointsList.Remove(item);
        }

        public IEnumerator<CurvePoint> GetEnumerator()
        {
            return pointsList.GetEnumerator();
        }
        #endregion
    }

    public enum polynomialCurveType
    {
        Lagrange_Linear = 1,
        Lagrange_Quadratic = 2,
        Newton = 3
    }
}
