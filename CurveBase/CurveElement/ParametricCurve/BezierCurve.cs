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
using System.Diagnostics;
using System.Text;

using Util.Tool;
using Util.Variable;
using Util.Variable.PointList;

namespace CurveBase.CurveElement.ParametricCurve
{
    public class BezierCurve
    {
        private List<int> combination = null;
        private NormalCurvePointList pointList = null;

        #region Constructor
        public BezierCurve(NormalCurvePointList pointList)
        {
            this.pointList = pointList;
            combination = new List<int>();
            updateCombination(pointList.Count - 1);
        }
        #endregion

        #region Public.Interface
        public DataPoint calculatePoint(DoubleExtension doubleExtension)
        {
            Debug.Assert(doubleExtension <= 1.0001 && doubleExtension >= -0.0001, "Invalid argument for BezierCurve.calculate()");
            double xVal = 0;
            double yVal = 0;
            double basisFunction = 1;
            for (int i = 0; i < pointList.Count; i++)
            {
                basisFunction = calculateBasisFunction(doubleExtension, pointList.Count - 1, i).AccurateValue;
                xVal += basisFunction * pointList[i].X.AccurateValue;
                yVal += basisFunction * pointList[i].Y.AccurateValue;
            }
            return new DataPoint(xVal, yVal);
        }
        #endregion

        #region Property
        public DataPoint LastPoint
        {
            get
            {
                return pointList.LastPoint;
            }
        }
        #endregion

        #region Private.Methods
        private void updateCombination(int count)
        {
            combination.Clear();
            for (int i = 0; i <= count; i++)
            {
                combination.Add(MathExtension.combination(count, i));
            }
        }

        private DoubleExtension calculateBasisFunction(DoubleExtension doubleExtension, int big, int small)
        {
            Debug.Assert(doubleExtension < 1.0001 && doubleExtension > -0.0001 && big >= small, "Invalid argument for BezierCurve.calculateOne()");
            return new DoubleExtension(combination[small] * Math.Pow(doubleExtension.AccurateValue, small) * Math.Pow(1 - doubleExtension.AccurateValue, big - small));
        }
        #endregion
    }
}
