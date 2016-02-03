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

using CurvePane.Extension.Variable;
using CurvePane.Curve.CurveElements.Point;

namespace CurvePane.Extension.Tool
{
    public static class MathExtension
    {
        public static List<double> calculateLinearPolynomialCoefficients(CurvePoint pt1, CurvePoint pt2)
        {
            Debug.Assert(pt1.X == pt2.X, "The two points have the same X value.");
            List<double> coefficients = new List<double>();
            double a = ((pt2.Y - pt1.Y) / (pt2.X - pt1.X)).CoordinateValue;
            double b = ((pt1.Y + pt2.Y - a * (pt1.X + pt2.X)) / 2).CoordinateValue;
            coefficients.Add(b);
            coefficients.Add(a);
            return coefficients;
        }

        public static List<double> calculateQuadraticPolynomialCoefficients(CurvePoint pt1, CurvePoint pt2, CurvePoint pt3)
        {
            //TODO: calculate Quadratic Polynomial Coefficients
            throw new NotImplementedException();
        }
    }
}
