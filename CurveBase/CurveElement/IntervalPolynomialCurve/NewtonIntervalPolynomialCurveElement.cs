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

using Util.Variable;
using Util.Variable.Interval;
using Util.Variable.PointList;

namespace CurveBase.CurveElement.IntervalPolynomialCurve
{
    /// <summary>
    /// Polynomial Curve:
    /// P(x) = a[0] + a[1] * (x - x0) + a[2] * (x - x0) * (x - x1) + a[3] * (x - x0) * (x - x1) * (x - x2)
    /// </summary>
    public class NewtonIntervalPolynomialCurveElement : IntervalPolynomialCurveElement
    {
        protected static Dynamic2DArray<DoubleExtension> fullCoefficents = new Dynamic2DArray<DoubleExtension>();
        protected OrderedCurvePointList list = null;
        protected static NewtonIntervalPolynomialCurveElement nullNewtonPolynomialCurve = new NewtonIntervalPolynomialCurveElement();

        #region Constructor
        public NewtonIntervalPolynomialCurveElement(OrderedCurvePointList pointList)
        {
            if (degree >= 0)
            {
                this.degree = pointList.Count;
                UpdateCoefficients(pointList);
            }
            list = pointList;
            this.interval = new DataInterval(list.LeftBorderPoint.X, list.RightBorderPoint.X);
        }

        private NewtonIntervalPolynomialCurveElement()
        {
            this.degree = 0;
            this.list = new OrderedCurvePointList();
            this.interval = DataInterval.NullDataInterval;
        }
        #endregion

        #region IntervalPolynomialCurve Member
        public override DoubleExtension calculate(DoubleExtension doubleExtension)
        {
            if (!interval.IsBetweenBordersCloseInterval(doubleExtension))
                throw new ArgumentOutOfRangeException("doubleExtension", "The value given is out of borders of intervals. Value: " + doubleExtension.ApproximateString + ", Range: [" + interval.LeftBorder.ApproximateString + ", " + interval.RightBorder.ApproximateString + "].");
            double result = 0;
            double poweredX = 1;
            for (int i = 0; i < degree; i++)
            {
                result += poweredX * fullCoefficents.GetArrayElement(i, 0).AccurateValue;
                poweredX *= doubleExtension.AccurateValue - list[i].X.AccurateValue;
            }
            return new DoubleExtension(result);
        }
        #endregion

        #region Private.Methods
        private void UpdateCoefficients(ICurvePointList pointList)
        {
            for (int i = 0; i < degree; i++)
            {
                fullCoefficents.SetArrayElement(0, i, pointList[i].Y);
            }
            for (int i = 1; i < degree; i++)
                for (int j = 0; j < degree - i; j++)
                    fullCoefficents.SetArrayElement(i, j, new DoubleExtension((fullCoefficents.GetArrayElement(i - 1, j + 1).AccurateValue - fullCoefficents.GetArrayElement(i - 1, 0).AccurateValue) / (pointList[i + j].X.AccurateValue - pointList[i - 1].X.AccurateValue)));
        }
        #endregion
    }
}
