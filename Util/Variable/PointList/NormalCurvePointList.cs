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
    public class NormalCurvePointList : ICurvePointList
    {
        protected List<DataPoint> points;
        protected string label = "";
        protected PaneCurveType paneCurveType = PaneCurveType.unknown;

        #region Constructor
        public NormalCurvePointList(List<DataPoint> points)
        {
            this.points = points;
        }

        public NormalCurvePointList()
        {
            points = new List<DataPoint>();
        }
        #endregion

        #region Property
        public DataPoint LastPoint
        {
            get
            {
                return points[points.Count - 1];
            }
        }
        #endregion

        #region Public Member
        public void Insert(int index, DataPoint item)
        {
            points.Insert(index, item);
        }

        public void AddRange(List<DataPoint> points)
        {
            foreach (DataPoint item in points)
            {
                this.points.Add(item);
            }
        }
        #endregion

        #region ICurvePointList Member
        public int IndexOf(DataPoint item)
        {
            return points.IndexOf(item);
        }

        public void RemoveAt(int index)
        {
            points.RemoveAt(index);
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
        }

        public void Clear()
        {
            points.Clear();
        }

        public bool Contains(DataPoint item)
        {
            return points.Contains(item);
        }

        public void CopyTo(DataPoint[] array, int arrayIndex)
        {
            points.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return points.Count; }
        }

        public bool Remove(DataPoint item)
        {
            return points.Remove(item);
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
    }
}
