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
using CurveBase.CurveElement.IntervalCurve;
using Util.Variable;

namespace CurveBase.CurveData.CurveInterpolatedData
{
    public class NewtonPolynomialCurveInterpolatedData : ICurveInterpolatedData
    {
        private int size = 0;
        private NewtonPolynomialCurve newtonCurve = null;
        #region Constructor
        public NewtonPolynomialCurveInterpolatedData(polynomialCurveParam curveParam)
        {
            size = curveParam.PointList.Count + 1;
            
            
        }
        #endregion

        #region ICurveInterpolatedData Member
        public CurveType getCurveType()
        {
            return CurveType.polynomialCurve;
        }

        public DataPoint getLastPoint()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
