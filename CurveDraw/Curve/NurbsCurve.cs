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
using CurveBase.CurveData.CurveParam;
using CurveBase.CurveData.CurveInterpolatedData;
using CurveBase.CurveElement.ParametricCurve;
using CurveBase.CurveException;
using CurveDraw.Draw;
using Util.Enum;
using Util.Variable;
using Util.Variable.PointList;

namespace CurveDraw.Curve
{
    public class NurbsCurve : ICurve
    {
        private NurbsCurveParam curveParam;
        private bool canDraw = false;
        
        #region Constructor
        public NurbsCurve(ICurveParam curveParam)
        {
            if (canDrawCurve(curveParam))
            {
                canDraw = true;
                this.curveParam = (NurbsCurveParam)curveParam;
            }
        }
        #endregion

        #region ICurve Member
        public Dictionary<ICurvePointList, DrawType> sampleCurvePoints()
        {
            NormalCurvePointList list = new NormalCurvePointList();
            Dictionary<ICurvePointList, DrawType> result = new Dictionary<ICurvePointList, DrawType>();

            NurbsCurveInterpolatedData data = new NurbsCurveInterpolatedData(curveParam);
            list.AddRange(sampleANurbsCurve(data.Curve));
            list.Add(data.getLastPoint());
            list.Label = "[NB]";
            list.PaneCurveType = PaneCurveType.realCurve;
            result.Add(list, DrawType.LineNoDot);

            curveParam.PointList.Label = "[NBC]";
            curveParam.PointList.PaneCurveType = PaneCurveType.connectingSupportingCurve;
            result.Add(curveParam.PointList, DrawType.LineNoDot);
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
            if (curveParam.getCurveType() == InterpolationCurveType.nurbsCurve)
            {
                NurbsCurveParam param = (NurbsCurveParam)curveParam;
                if (param.Count <= param.Degree)
                    throw new InvalidBasePointsException(InterpolationCurveType.nurbsCurve, (param.Degree - param.Count + 1).ToString() + " more data points are needed to draw NURBS Curve");
                if (param.Interval.CutPoints[param.Degree] == param.Interval.CutPoints[param.PointList.Count])
                    throw new InvalidBasePointsException(InterpolationCurveType.bsCurve, "The degree is too high or the base points are insufficient or too many duplicated nodes are specified.");
            }
            else
            {
                throw new UnmatchedCurveParamTypeException(InterpolationCurveType.nurbsCurve, curveParam.getCurveType());
            }
            return true;
        }

        private List<DataPoint> sampleANurbsCurve(NurbsParametricCurveElement curve)
        {
            double stepSize = 0.005;
            int step = 200;
            if (curve.Interval.Length > 1)
            {
                stepSize = curve.Interval.Length.AccurateValue / step;
            }
            else
            {
                step = (int)(curve.Interval.Length.AccurateValue / stepSize);
            }

            DoubleExtension parametricValue = curve.Interval.LeftBorder;
            List<DataPoint> pts = new List<DataPoint>();
            int count = 0;
            while (count++ < step)
            {
                pts.Add(curve.calculatePoint(parametricValue));
                parametricValue += stepSize;
            }
            return pts;
        }
        #endregion
    }
}
