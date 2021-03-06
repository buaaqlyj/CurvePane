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

using CurveBase;
using CurveBase.CurveData.CurveInterpolatedData;
using CurveBase.CurveData.CurveParam;
using CurveBase.CurveElement.ParametricCurve;
using CurveBase.CurveException;
using CurveDraw.Draw;
using Util.Enum;
using Util.Variable;
using Util.Variable.PointList;

namespace CurveDraw.Curve
{
    public class BezierCurve : ICurve
    {
        private BezierCurveParam curveParam;
        private bool canDraw = false;

        #region Constructor
        public BezierCurve(ICurveParam curveParam)
        {
            if (canDrawCurve(curveParam))
            {
                canDraw = true;
                this.curveParam = (BezierCurveParam)curveParam;
            }
        }
        #endregion

        #region ICurve Member
        public Dictionary<ICurvePointList, DrawType> sampleCurvePoints()
        {
            NormalCurvePointList list = new NormalCurvePointList();
            Dictionary<ICurvePointList, DrawType> result = new Dictionary<ICurvePointList, DrawType>();

            BezierCurveInterpolatedData data = new BezierCurveInterpolatedData(curveParam);
            list.AddRange(sampleABezierCurve(data.Curve));
            list.Add(data.getLastPoint());
            list.Label = "[BZ]";
            list.PaneCurveType = PaneCurveType.realCurve;
            result.Add(list, DrawType.LineNoDot);

            curveParam.PointList.Label = "[BZC]";
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
            if (curveParam.getCurveType() == InterpolationCurveType.bezierCurve)
            {
                BezierCurveParam param = (BezierCurveParam)curveParam;
                if (param.Count < 2)
                    throw new InvalidBasePointsException(InterpolationCurveType.bezierCurve, "At least two points are needed to draw Bezier Curve");
            }
            else
            {
                throw new UnmatchedCurveParamTypeException(InterpolationCurveType.bezierCurve, curveParam.getCurveType());
            }
            return true;
        }

        private List<DataPoint> sampleABezierCurve(BezierParametricCurveElement curve)
        {
            double stepSize = 0.002;
            int step = 500;
            double parametricValue = 0;
            List<DataPoint> pts = new List<DataPoint>();
            int count = 0;
            while (count++ < step)
            {
                pts.Add(curve.calculatePoint(new DoubleExtension(parametricValue)));
                parametricValue += stepSize;
            }
            return pts;
        }
        #endregion
    }
}
