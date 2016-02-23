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
    public class NurbsCurveInterpolatedData : BSplineCurveInterpolatedData
    {
        protected new NurbsParametricCurveElement curve = null;
        protected new NurbsCurveParam curveParam = null;

        #region Constructor
        public NurbsCurveInterpolatedData(NurbsCurveParam curveParam)
            : base(curveParam)
        {
            this.curveParam = curveParam;
            InitialCurve(curveParam);
        }
        #endregion

        #region Property
        public new NurbsParametricCurveElement Curve
        {
            get
            {
                return curve;
            }
        }
        #endregion

        #region ICurveInterpolatedData Member
        public override CurveType getCurveType()
        {
            return CurveType.nurbsCurve;
        }

        public override DataPoint getLastPoint()
        {
            return curve.calculatePoint(curveParam.Interval.CutPoints[curveParam.PointList.Count]);
        }
        #endregion

        #region Private.Methods
        protected override void InitialCurve(ICurveParam curveParam)
        {
            if (curveParam.getCurveType() == CurveType.nurbsCurve)
            {
                NurbsCurveParam param = (NurbsCurveParam)curveParam;
                this.curve = new NurbsParametricCurveElement(param);
            }
            else
            {
                throw new UnmatchedCurveParamTypeException(CurveType.nurbsCurve, curveParam.getCurveType());
            }
        }
        #endregion
    }
}
