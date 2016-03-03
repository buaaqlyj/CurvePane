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
using CurveBase.CurveElement.IntervalPolynomialCurve;
using CurveBase.CurveException;
using CurveBase.CurveData.CurveParam;
using CurveBase.CurveData.CurveInterpolatedData;
using CurveDraw.Draw;
using Util.Enum;
using Util.Tool;
using Util.Variable;
using Util.Variable.PointList;

namespace CurveDraw.Curve
{
    public class PolynomialCurve : ICurve
    {
        private PolynomialCurveParam curveParam;
        private bool canDraw = false;

        #region Constructor
        public PolynomialCurve(ICurveParam curveParam)
        {
            if (canDrawCurve(curveParam))
            {
                canDraw = true;
                this.curveParam = (PolynomialCurveParam)curveParam;
            }
        }
        #endregion

        #region ICurve Member
        public Dictionary<ICurvePointList, DrawType> sampleCurvePoints()
        {
            OrderedCurvePointList list = new OrderedCurvePointList();
            Dictionary<ICurvePointList, DrawType> result = new Dictionary<ICurvePointList, DrawType>();
            
            if (curveParam.PolynomialCurveType == PolynomialCurveType.Newton)
            {
                NewtonPolynomialCurveInterpolatedData data = new NewtonPolynomialCurveInterpolatedData(curveParam);
                list.AddRange(sampleAPolynomialCurve(data.Curve, 200));
                list.Add(data.getLastPoint());
                list.Label = "[NP]";
            }
            else
            {
                LagarangePolynomialCurveInterpolatedData data = new LagarangePolynomialCurveInterpolatedData(curveParam);
                if (data.PointList != null)
                {
                    foreach (Util.Variable.DataPoint item in data.PointList)
                    {
                        list.Add(item);
                    }
                }
                else if (data.Curves != null)
                {
                    //TODO: 更改取样方法
                    //foreach (NormalIntervalPolynomialCurveElement curve in data.Curves)
                    //{
                    //    list.AddRange(sampleAPolynomialCurve(curve, 50));
                    //}
                    list.Add(data.getLastPoint());
                }
                list.Label = "[LP]";
            }
            list.PaneCurveType = PaneCurveType.realCurve;
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
            if (curveParam.getCurveType() == InterpolationCurveType.polynomialCurve)
            {
                PolynomialCurveParam param = (PolynomialCurveParam)curveParam;
                if (param.Count < 2)
                    throw new InvalidBasePointsException(InterpolationCurveType.polynomialCurve, "At least two points are needed to draw Polynomial Interpolated Curve");
                if (!param.PointList.noDuplicatedX())
                    throw new InvalidBasePointsException(InterpolationCurveType.polynomialCurve, "At least two points given have the same X value.");
            }
            else
            {
                throw new UnmatchedCurveParamTypeException(InterpolationCurveType.polynomialCurve, curveParam.getCurveType());
            }
            return true;
        }

        private List<DataPoint> sampleAPolynomialCurve(IntervalPolynomialCurveElement curve, int maxPointCount)
        {
            if (maxPointCount < 2)
                throw new ArgumentOutOfRangeException("maxPointCount", "The parameter of sampleAPolynomialCurve is out of range.");
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