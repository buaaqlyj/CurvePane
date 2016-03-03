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

using CurveBase;
using CurveBase.CurveData.CurveInterpolatedData;
using CurveBase.CurveData.CurveParam;
using CurveBase.CurveElement.IntervalPolynomialCurve;
using CurveBase.CurveElement.ParametricCurve;
using CurveBase.CurveException;
using CurveDraw.Draw;
using Util.Enum;
using Util.Variable;
using Util.Variable.PointList;

namespace CurveDraw.Curve
{
    public class ParametricCubicSplineInterpolationCurve : ICurve
    {
        private ParametricCubicSplineInterpolationCurveParam curveParam;
        private bool canDraw = false;

        #region Constructor
        public ParametricCubicSplineInterpolationCurve(ICurveParam curveParam)
        {
            if (canDrawCurve(curveParam))
            {
                canDraw = true;
                this.curveParam = (ParametricCubicSplineInterpolationCurveParam)curveParam;
            }
        }
        #endregion

        #region ICurve Member
        public Dictionary<ICurvePointList, DrawType> sampleCurvePoints()
        {
            NormalCurvePointList list = new NormalCurvePointList();
            Dictionary<ICurvePointList, DrawType> result = new Dictionary<ICurvePointList, DrawType>();

            ParametricCubicSplineInterpolationInterpolatedData data = new ParametricCubicSplineInterpolationInterpolatedData(curveParam);
            list.AddRange(sampleAParametricCurveWithDenserBorder(data.Curve, 1000));
            list.Add(data.getLastPoint());
            list.Label = "[PCSI]";
            list.PaneCurveType = PaneCurveType.realCurve;
            result.Add(list, DrawType.LineNoDot);
            return result;
        }

        public bool CanDraw
        {
            get { return canDraw; }
        }
        #endregion

        #region Private Member
        private bool canDrawCurve(ICurveParam curveParam)
        {
            if (curveParam.getCurveType() == InterpolationCurveType.pcsiCurve)
            {
                ParametricCubicSplineInterpolationCurveParam param = (ParametricCubicSplineInterpolationCurveParam)curveParam;
                if (param.Count < 2)
                    throw new InvalidBasePointsException(InterpolationCurveType.pcsiCurve, "At least two points are needed to draw Parametric Cubic Spline Interpolation Curve");
            }
            else
            {
                throw new UnmatchedCurveParamTypeException(InterpolationCurveType.pcsiCurve, curveParam.getCurveType());
            }
            return true;
        }

        private List<DataPoint> sampleAParametricCurveWithDenserBorder(ParametricCubicSplineInterpolationCurveElement curve, int maxPointCount)
        {
            if (maxPointCount < 2)
                throw new ArgumentOutOfRangeException("maxPointCount", "The desired point count to sample a curve should be bigger than 1.");
            double normalStepSize = 0.001, borderStepSize = 0.0005;
            int step, stage = 1;
            if (curve.Interval.Length > 0.2)
            {
                stage = 1;
                if (curve.Interval.Length.AccurateValue > 0.001 * maxPointCount)
                {
                    normalStepSize = (curve.Interval.Length.AccurateValue - 0.2) / maxPointCount;
                    step = maxPointCount + 400;
                }
                else
                {
                    step = (int)(curve.Interval.Length.AccurateValue * 1000) + 200;
                }
            }
            else
            {
                stage = 3;
                step = Convert.ToInt32(curve.Interval.Length.AccurateValue * 2000);
            }

            DoubleExtension parametricValue = curve.Interval.LeftBorder;
            List<DataPoint> pts = new List<DataPoint>();
            while (parametricValue < curve.Interval.RightBorder)
            {
                pts.Add(curve.calculatePoint(parametricValue));
                switch (stage)
                {
                    case 1:
                        if (curve.Interval.RightBorder - parametricValue <= 0.1)
                        {
                            stage = 3;
                        }
                        else if (parametricValue - curve.Interval.LeftBorder >= 0.1)
                        {
                            stage = 2;
                            parametricValue += normalStepSize;
                            break;
                        }
                        parametricValue += borderStepSize;
                        break;
                    case 2:
                        if (curve.Interval.RightBorder - parametricValue <= 0.1)
                        {
                            stage = 3;
                            parametricValue += borderStepSize;
                            break;
                        }
                        parametricValue += normalStepSize;
                        break;
                    case 3:
                        parametricValue += borderStepSize;
                        break;
                }
            }
            return pts;
        }
        #endregion
    }
}
