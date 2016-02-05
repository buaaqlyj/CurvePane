using System;
using System.Collections.Generic;
using System.Text;

using Util.Variable;

namespace Util.Variable.PointList
{
    public class BaseDataPointList : ICurvePointList
    {
        protected List<DataPoint> points;
        protected SortedList<DoubleExtension, DataPoint> sortedPointList;

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

        public bool IsReadOnly
        {
            get { return false; }
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
            points.Add(item);
            sortedPointList.Add(item.X, item);
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
            return sortedPointList.Remove(item.X) && points.Remove(item);
        }

        public IEnumerator<DataPoint> GetEnumerator()
        {
            return points.GetEnumerator();
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

        public SortedList<DoubleExtension, DataPoint> SortedPointList
        {
            get
            {
                return sortedPointList;
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
