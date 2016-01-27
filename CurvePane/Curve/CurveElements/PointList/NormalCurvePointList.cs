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
