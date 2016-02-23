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

using Util.Variable;

namespace CurveBase.CurveData.CurveParam
{
    public class NurbsCurveParam : BSplineCurveParam
    {
        protected List<DoubleExtension> weightList;
        
        #region Constructor
        public NurbsCurveParam(List<DataPoint> points, int degree, List<DoubleExtension> cutPoints, List<DoubleExtension> weightList)
            : base(points, degree, cutPoints)
        {
            if (points.Count != weightList.Count)
            {
                throw new ArgumentException("The count of weights doesn't match with that of base points");
            }
            this.weightList = weightList;
        }
        #endregion

        #region Property
        public List<DoubleExtension> Weight
        {
            get
            {
                return weightList;
            }
        }
        #endregion

        #region ICurveParam Member
        public override CurveType getCurveType()
        {
            return CurveType.nurbsCurve;
        }
        #endregion
    }
}
