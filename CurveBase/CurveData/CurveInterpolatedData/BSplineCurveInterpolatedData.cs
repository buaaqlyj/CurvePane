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

using CurveBase.CurveData.CurveParam;
using CurveBase.CurveElement.IntervalPolynomialCurve;
using CurveBase.CurveElement.ParametricCurve;
using CurveBase.CurveException;
using Util.Variable;
using Util.Variable.Interval;
using Util.Variable.PointList;

namespace CurveBase.CurveData.CurveInterpolatedData
{
    public class BSplineCurveInterpolatedData : ICurveInterpolatedData
    {
        protected PiecewiseDataInterval interval;
        protected BSplineParametricCurveElement curve = null;
        protected BSplineCurveParam curveParam = null;

        #region Constructor
        public BSplineCurveInterpolatedData(BSplineCurveParam curveParam)
        {
            this.curveParam = curveParam;
            this.interval = curveParam.Interval;
            InitialCurve(curveParam);
        }
        #endregion

        #region Property
        public BSplineParametricCurveElement Curve
        {
            get
            {
                return curve;
            }
        }
        #endregion

        #region ICurveInterpolatedData Member
        public virtual InterpolationCurveType getCurveType()
        {
            return InterpolationCurveType.bsCurve;
        }

        public virtual DataPoint getLastPoint()
        {
            return curve.calculatePoint(curveParam.Interval.CutPoints[curveParam.PointList.Count]);
        }
        #endregion

        #region Private.Methods
        protected virtual void InitialCurve(ICurveParam curveParam)
        {
            if (curveParam.getCurveType() == InterpolationCurveType.bsCurve)
            {
                BSplineCurveParam param = (BSplineCurveParam)curveParam;
                this.curve = new BSplineParametricCurveElement(param);
            }
            else
            {
                throw new UnmatchedCurveParamTypeException(InterpolationCurveType.bsCurve, curveParam.getCurveType());
            }
        }
        #endregion
    }
}
