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

using CurveBase;
using CurveBase.CurveData.CurveInterpolatedData;
using CurveBase.CurveData.CurveParam;
using CurveBase.CurveElement.IntervalPolynomialCurve;
using CurveBase.CurveException;
using CurveDraw.Draw;
using Util.Variable;
using Util.Variable.PointList;

namespace CurveDraw.Curve
{
    public class CubicSplineInterpolationCurve : ICurve
    {
        private CubicSplineInterpolationCurveParam curveParam;
        private bool canDraw = false;
        
        #region Constructor
        public CubicSplineInterpolationCurve(ICurveParam curveParam)
        {
            if (canDrawCurve(curveParam))
            {
                canDraw = true;
                this.curveParam = (CubicSplineInterpolationCurveParam)curveParam;
            }
        }
        #endregion

        #region ICurve Member
        public Dictionary<ICurvePointList, DrawType> sampleCurvePoints()
        {
            OrderedCurvePointList list = new OrderedCurvePointList();
            Dictionary<ICurvePointList, DrawType> result = new Dictionary<ICurvePointList, DrawType>();

            CubicSplineInterpolationInterpolatedData data = new CubicSplineInterpolationInterpolatedData(curveParam);
            foreach (LagarangeIntervalPolynomialCurveElement curve in data.Curves)
            {
                list.AddRange(sampleAPolynomialCurve(curve, 50));
            }
            list.Add(data.getLastPoint());
            list.Label = "[CSI]";
            result.Add(list, DrawType.LineNoDot);
            return result;
        }

        public bool CanDraw
        {
            get { return canDraw; }
        }
        #endregion

        #region Private.Methods
        private bool canDrawCurve(ICurveParam curveParam)
        {
            if (curveParam.getCurveType() == CurveType.csiCurve)
            {
                CubicSplineInterpolationCurveParam param = (CubicSplineInterpolationCurveParam)curveParam;
                if (param.Count < 2)
                    throw new InvalidBasePointsException(CurveType.csiCurve, "At least two points are needed to draw Cubic Spline Interpolation Curve");
                if (!param.PointList.noDuplicatedX())
                    throw new InvalidBasePointsException(CurveType.csiCurve, "At least two points given have the same X value.");
            }
            else
            {
                throw new UnmatchedCurveParamTypeException(CurveType.csiCurve, curveParam.getCurveType());
            }
            return true;
        }

        private List<DataPoint> sampleAPolynomialCurve(IntervalPolynomialCurveElement curve, int maxPointCount)
        {
            if (maxPointCount < 2)
                throw new ArgumentOutOfRangeException("maxPointCount", "The desired point count to sample a curve should be bigger than 1.");
            double stepSize;
            int step;
            if (curve.Interval.Length.AccurateValue > 0.001 * maxPointCount)
            {
                stepSize = curve.Interval.Length.AccurateValue / maxPointCount;
                step = maxPointCount;
            }
            else
            {
                stepSize = 0.001;
                step = (int)(curve.Interval.Length.AccurateValue * 1000);
            }
            double xValue = curve.Interval.LeftBorder.AccurateValue;
            List<DataPoint> pts = new List<DataPoint>();
            int count = 0;
            while (count < step)
            {
                pts.Add(new DataPoint(xValue, curve.calculate(new DoubleExtension(xValue)).AccurateValue));
                xValue += stepSize;
                count++;
            }
            return pts;
        }
        #endregion
    }
}
