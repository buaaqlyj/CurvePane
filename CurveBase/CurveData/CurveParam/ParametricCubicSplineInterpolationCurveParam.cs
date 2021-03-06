﻿/// Copyright 2016 Troy Lewis. Some Rights Reserved
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
    public class ParametricCubicSplineInterpolationCurveParam : ICurveParam, ICurvePointList
    {
        private NormalCurvePointList pointList;
        private CubicSplineInterpolationCurveParam xList, yList;
        private PiecewiseDataInterval interval;

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="points">型值点</param>
        /// <param name="curveType">边界条件类型</param>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <param name="val3"></param>
        /// <param name="val4"></param>
        public ParametricCubicSplineInterpolationCurveParam(List<DataPoint> points, PCSIBorderConditionType curveType, DoubleExtension val1, DoubleExtension val2, DoubleExtension val3, DoubleExtension val4)
        {
            DoubleExtension subValLeftX = DoubleExtension.ZERO, subValRightX = DoubleExtension.ZERO, subValLeftY = DoubleExtension.ZERO, subValRightY = DoubleExtension.ZERO;
            CSIBorderConditionType subType = CSIBorderConditionType.First_Order_Derivative;
            List<DataPoint> xPointList = new List<DataPoint>();
            List<DataPoint> yPointList = new List<DataPoint>();

            if (points.Count < 2)
                throw new ArgumentException("At least two points are needed for PCSI Curve drawing.");

            pointList = new NormalCurvePointList(points);

            xPointList.Add(new DataPoint(DoubleExtension.ZERO, points[0].X));
            yPointList.Add(new DataPoint(DoubleExtension.ZERO, points[0].Y));
            DoubleExtension accumulatedChordLength = DoubleExtension.ZERO;
            List<DoubleExtension> cutPoints = new List<DoubleExtension>();
            cutPoints.Add(accumulatedChordLength);
            for (int i = 1; i < points.Count; i++)
            {
                accumulatedChordLength += DataPoint.CalculateDistance(points[i - 1], points[i]);
                cutPoints.Add(accumulatedChordLength);
                xPointList.Add(new DataPoint(accumulatedChordLength, points[i].X));
                yPointList.Add(new DataPoint(accumulatedChordLength, points[i].Y));
            }

            this.interval = new PiecewiseDataInterval(cutPoints);

            switch (curveType)
            {
                case PCSIBorderConditionType.First_Order_Derivative:
                    subType = CSIBorderConditionType.First_Order_Derivative;
                    DoubleExtension arcLeft1 = new DoubleExtension(Math.Atan(val1.AccurateValue));
                    DoubleExtension arcRight1 = new DoubleExtension(Math.Atan(val2.AccurateValue));
                    subValLeftX = new DoubleExtension(Math.Cos(arcLeft1.AccurateValue));
                    subValLeftY = new DoubleExtension(Math.Sin(arcLeft1.AccurateValue));
                    subValRightX = new DoubleExtension(Math.Cos(arcRight1.AccurateValue));
                    subValRightY = new DoubleExtension(Math.Sin(arcRight1.AccurateValue));
                    //DataVector referenceArcLeft1 = new DataVector(points[0], points[1]);
                    //DataVector referenceArcRight1 = new DataVector(points[points.Count - 2], points[points.Count - 1]);
                    //bool flagLeft = referenceArcLeft.GetTheFlagForCloserArc(arcLeft);
                    //bool flagRight = referenceArcRight.GetTheFlagForCloserArc(arcRight);
                    //if (flagLeft)
                    //{
                    //    subValLeftX = new DoubleExtension(Math.Cos(arcLeft.AccurateValue));
                    //    subValLeftY = new DoubleExtension(Math.Sin(arcLeft.AccurateValue));
                    //}
                    //else
                    //{
                    //    subValLeftX = new DoubleExtension(0 - Math.Cos(arcLeft.AccurateValue));;
                    //    subValLeftY = new DoubleExtension(0 - Math.Sin(arcLeft.AccurateValue));
                    //}
                    //if (flagRight)
                    //{
                    //    subValRightX = new DoubleExtension(Math.Cos(arcRight.AccurateValue));
                    //    subValRightY = new DoubleExtension(Math.Sin(arcRight.AccurateValue));
                    //}
                    //else
                    //{
                    //    subValRightX = new DoubleExtension(Math.Cos(0 - arcRight.AccurateValue));
                    //    subValRightY = new DoubleExtension(Math.Sin(0 - arcRight.AccurateValue));
                    //}
                    break;
                case PCSIBorderConditionType.Zero_Curvature:
                    subType = CSIBorderConditionType.Second_Order_Derivative;
                    break;
                case PCSIBorderConditionType.Centre_of_Curvature:
                    if (val1 == points[0].X && val3 == points[0].Y)
                    {
                        throw new ArgumentException("The center of the left border's curvature is the same as the left border point.");
                    }
                    if (val2 == points[points.Count - 1].X && val4 == points[points.Count - 1].Y)
                    {
                        throw new ArgumentException("The center of the right border's curvature is the same as the right border point.");
                    }
                    
                    //
                    // Second_Order_Derivative
                    //
                    //subType = CSIBorderConditionType.Second_Order_Derivative;
                    //DoubleExtension denominator1 = new DoubleExtension(Math.Pow(val1.AccurateValue - points[0].X.AccurateValue, 2) + Math.Pow(val3.AccurateValue - points[0].Y.AccurateValue, 2));
                    //DoubleExtension denominator2 = new DoubleExtension(Math.Pow(val2.AccurateValue - points[points.Count - 1].X.AccurateValue, 2) + Math.Pow(val4.AccurateValue - points[points.Count - 1].Y.AccurateValue, 2));
                    //subValLeftX = (val1 - points[0].X) / denominator1;
                    //subValLeftY = (val3 - points[0].Y) / denominator1;
                    //subValRightX = (val2 - points[points.Count - 1].X) / denominator2;
                    //subValRightY = (val4 - points[points.Count - 1].Y) / denominator2;

                    //
                    // First_Order_Derivative
                    //
                    subType = CSIBorderConditionType.First_Order_Derivative;
                    DoubleExtension arcLeft2 = new DoubleExtension(Math.Atan2(val3.AccurateValue - points[0].Y.AccurateValue, val1.AccurateValue - points[0].X.AccurateValue) + Math.PI / 2.0);
                    DoubleExtension arcRight2 = new DoubleExtension(Math.Atan2(val4.AccurateValue - points[points.Count - 1].Y.AccurateValue, val2.AccurateValue - points[points.Count - 1].X.AccurateValue) + Math.PI / 2.0);
                    subValLeftX = new DoubleExtension(Math.Cos(arcLeft2.AccurateValue));
                    subValLeftY = new DoubleExtension(Math.Sin(arcLeft2.AccurateValue));
                    subValRightX = new DoubleExtension(Math.Cos(arcRight2.AccurateValue));
                    subValRightY = new DoubleExtension(Math.Sin(arcRight2.AccurateValue));
                    break;
            }
            xList = new CubicSplineInterpolationCurveParam(xPointList, subType, subValLeftX, subValRightX);
            yList = new CubicSplineInterpolationCurveParam(yPointList, subType, subValLeftY, subValRightY);
        }
        #endregion

        #region Property
        public CubicSplineInterpolationCurveParam XParam
        {
            get
            {
                return xList;
            }
        }

        public CubicSplineInterpolationCurveParam YParam
        {
            get
            {
                return yList;
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
            return InterpolationCurveType.pcsiCurve;
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

    public enum PCSIBorderConditionType
    {
        First_Order_Derivative = 1,
        Zero_Curvature = 2,
        Centre_of_Curvature = 3
    }
}
