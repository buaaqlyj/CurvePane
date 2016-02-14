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

using CurveBase.CurveData.CurveParamData;
using CurveBase.CurveElement.ParametricCurve;
using Util.Variable;
using Util.Variable.PointList;

namespace CurveBase.CurveData.CurveInterpolatedData
{
    public class BezierCurveInterpolatedData : ICurveInterpolatedData
    {
        private BezierCurve bezierCurve = null;
        private NormalCurvePointList pointList = null;

        #region Constructor
        public BezierCurveInterpolatedData(bezierCurveParam curveParam)
        {
            pointList = curveParam.PointList;
            bezierCurve = new BezierCurve(pointList);
        }
        #endregion

        #region Property
        public NormalCurvePointList PointList
        {
            get
            {
                return pointList;
            }
        }

        public BezierCurve Curve
        {
            get
            {
                return bezierCurve;
            }
        }
        #endregion

        #region ICurveInterpolatedData
        public CurveType getCurveType()
        {
            return CurveType.bezierCurve;
        }

        public DataPoint getLastPoint()
        {
            return bezierCurve.LastPoint;
        }
        #endregion
    }
}
