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
