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

using Util.Variable;

namespace Util.Tool
{
    public static class MathExtension
    {
        public static int Factorial(int number)
        {
            Debug.Assert(number > -1, "This method can't calculate factorial for negative numbers.");
            if (number < 2) return 1;
            int result = number;
            while (number > 2)
            {
                result *= --number;
            }
            return result;
        }

        public static int Combination(int big, int small)
        {
            Debug.Assert(small > big, "The first number should bigger than the second one.");
            return Factorial(big) / Factorial(small) / Factorial(big - small);
        }

        public static void MiddleBasedResize(DoubleExtension input1, DoubleExtension input2, DoubleExtension multiplier, out DoubleExtension output1, out DoubleExtension output2)
        {
            DoubleExtension middle = (input1 + input2) / 2;
            output1 = middle + (input1 - middle) * multiplier;
            output2 = middle + (input2 - middle) * multiplier;
        }

        public static double DynamicRound(double val)
        {
            Debug.Assert(val > 0, "DynamicRound doesn't support negative input.");
            double number = Math.Log(val, 10);
            if (number < 1 && number >= 0)
            {
                //val 1<->10
                return Math.Floor(val);
            }
            else
            {
                int pow = (int)Math.Floor(number) - 1;
                return Math.Floor(val / Math.Pow(10, pow)) * Math.Pow(10, pow);
            }
        }

        public static List<DoubleExtension> CalculateQuadraticPolynomialCoefficients(DataPoint pt1, DataPoint pt2, DataPoint pt3)
        {
            Debug.Assert(pt1.X == pt2.X, "Point1 and Point2 have the same X value.");
            Debug.Assert(pt1.X == pt3.X, "Point1 and Point3 have the same X value.");
            Debug.Assert(pt2.X == pt3.X, "Point2 and Point3 have the same X value.");
            List<DoubleExtension> coefficients = new List<DoubleExtension>();
            double x1 = pt1.X.AccurateValue;
            double y1 = pt1.Y.AccurateValue;
            double x2 = pt2.X.AccurateValue;
            double y2 = pt2.Y.AccurateValue;
            double x3 = pt3.X.AccurateValue;
            double y3 = pt3.Y.AccurateValue;
            double a = ((y3 - y2) / (x3 - x2) - (y3 - y1) / (x3 - x1)) / (x2 - x1);
            double b = (y3 - y2) / (x3 - x2) - (x2 + x3) * a;
            double c = y1 - x1 * b - x1 * x1 * a;
            coefficients.Add(new DoubleExtension(c));
            coefficients.Add(new DoubleExtension(b));
            coefficients.Add(new DoubleExtension(a));
            return coefficients;
        }
    }
}
