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
using CurveBase.CurveElement.IntervalCurve;
using CurveBase.CurveException;
using CurveBase.CurveData.CurveParamData;
using CurveBase.CurveData.CurveInterpolatedData;
using CurveDraw.Draw;
using Util.Tool;
using Util.Variable;
using Util.Variable.PointList;

namespace CurveDraw.Curve
{
    public class polynomialCurve : ICurve
    {
        private polynomialCurveParam curveParam;
        private bool canDraw = false;

        #region ICurve Member

        public polynomialCurve(ICurveParam curveParam)
        {
            if (canDrawCurve(curveParam))
            {
                canDraw = true;
                this.curveParam = (polynomialCurveParam)curveParam;
            }
        }

        public Dictionary<ICurvePointList, DrawType> sampleCurvePoints()
        {
            OrderedCurvePointList list = new OrderedCurvePointList();
            Dictionary<ICurvePointList, DrawType> result = new Dictionary<ICurvePointList, DrawType>();
            
            if (curveParam.PolynomialCurveType == polynomialCurveType.Newton)
            {
                NewtonPolynomialCurveInterpolatedData data = new NewtonPolynomialCurveInterpolatedData(curveParam);
                list.AddRange(sampleAPolynomialCurve(data.Curve, 200));
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
                    foreach (LagarangePolynomialCurve curve in data.Curves)
                    {
                        list.AddRange(sampleAPolynomialCurve(curve, 50));
                    }
                    list.Add(data.getLastPoint());
                }
                list.Label = "[LP]";
            }
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
            if (curveParam.getCurveType() == CurveType.polynomialCurve)
            {
                polynomialCurveParam param = (polynomialCurveParam)curveParam;
                if (!param.PointList.noDuplicatedX())
                    throw new SameXInOrderedCurvePointListException(CurveType.polynomialCurve);
            }
            else
            {
                throw new UnmatchedCurveParamTypeException(CurveType.polynomialCurve, curveParam.getCurveType());
            }
            return true;
        }

        private List<DataPoint> sampleAPolynomialCurve(IntervalPolynomialCurve curve, int maxPointCount)
        {
            if (maxPointCount < 2)
                throw new ArgumentOutOfRangeException("maxPointCount", "The parameter of sampleAPolynomialCurve is out of range.");
            double stepSize;
            int step;
            if (curve.Interval.Length.CoordinateValue > 0.001 * maxPointCount)
            {
                stepSize = curve.Interval.Length.CoordinateValue / maxPointCount;
                step = maxPointCount;
            }
            else
            {
                stepSize = 0.001;
                step = (int)(curve.Interval.Length.CoordinateValue * 1000);
            }
            double xValue = curve.Interval.LeftBorder.CoordinateValue;
            List<DataPoint> pts = new List<DataPoint>();
            int count = 0;
            while (count < step)
            {
                pts.Add(new DataPoint(xValue, curve.calculate(xValue)));
                xValue += stepSize;
                count++;
            }
            return pts;
        }
        #endregion
    }
}