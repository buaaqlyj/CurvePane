using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using CurvePane.Curve.CurveElements.Point;

namespace CurvePane.Curve.CurveElements.PointList
{
    public interface ICurvePointList
    {
        int IndexOf(CurvePoint item);

        void RemoveAt(int index);

        CurvePoint this[int index] { get; set; }

        void Add(CurvePoint item);

        void Clear();

        bool Contains(CurvePoint item);

        void CopyTo(CurvePoint[] array, int arrayIndex);

        int Count { get; }

        bool IsReadOnly { get; }

        bool Remove(CurvePoint item);

        IEnumerator<CurvePoint> GetEnumerator();
    }
}