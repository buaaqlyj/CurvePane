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

using Util.Enum;
using Util.Variable;
using Util.Variable.Interval;
using Util.Variable.PointList;

namespace CurveBase.CurveData.CurveParam
{
    public class CubicSplineInterpolationCurveParam : ICurveParam, ICurvePointList
    {
        private OrderedCurvePointList pointList;
        private DoubleExtension leftVal, rightVal;
        private CSIBorderConditionType csiConditionType;
        private PiecewiseDataInterval interval;

        #region Constructor
        public CubicSplineInterpolationCurveParam(List<DataPoint> points, CSIBorderConditionType curveType, DoubleExtension val1, DoubleExtension val2)
        {
            pointList = new OrderedCurvePointList(points);
            csiConditionType = curveType;
            leftVal = val1;
            rightVal = val2;
            interval = new PiecewiseDataInterval(pointList);
        }
        #endregion

        #region Property
        public OrderedCurvePointList PointList
        {
            get
            {
                return pointList;
            }
        }

        public CSIBorderConditionType BorderConditionType
        {
            get
            {
                return csiConditionType;
            }
        }

        public DoubleExtension LeftBorderValue
        {
            get
            {
                return leftVal;
            }
        }

        public DoubleExtension RightBorderValue
        {
            get
            {
                return rightVal;
            }
        }

        public PiecewiseDataInterval Interval
        {
            get
            {
                return interval;
            }
        }
        #endregion

        #region ICurveParam Member
        public InterpolationCurveType getCurveType()
        {
            return InterpolationCurveType.csiCurve;
        }
        #endregion

        #region ICurvePointList Member
        public int IndexOf(DataPoint item)
        {
            return pointList.IndexOf(item);
        }

        public void RemoveAt(int index)
        {
            pointList.RemoveAt(index);
        }

        public DataPoint this[int index]
        {
            get
            {
                return pointList[index];
            }
            set
            {
                pointList[index] = value;
            }
        }

        public void Add(DataPoint item)
        {
            pointList.Add(item);
        }

        public void Clear()
        {
            pointList.Clear();
        }

        public bool Contains(DataPoint item)
        {
            return pointList.Contains(item);
        }

        public void CopyTo(DataPoint[] array, int arrayIndex)
        {
            pointList.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return pointList.Count; }
        }

        public bool Remove(DataPoint item)
        {
            return pointList.Remove(item);
        }

        public IEnumerator<DataPoint> GetEnumerator()
        {
            return pointList.GetEnumerator();
        }

        public string Label
        {
            get
            {
                return pointList.Label;
            }
            set
            {
                pointList.Label = value;
            }
        }

        public PaneCurveType PaneCurveType
        {
            get
            {
                return pointList.PaneCurveType;
            }
            set
            {
                pointList.PaneCurveType = value;
            }
        }

        public List<DoubleExtension> XList
        {
            get { return pointList.XList; }
        }

        public List<DoubleExtension> YList
        {
            get { return pointList.YList; }
        }
        #endregion
    }

    public enum CSIBorderConditionType
    {
        First_Order_Derivative = 1,
        Second_Order_Derivative = 2,
        Cyclicity = 3
    }
}
