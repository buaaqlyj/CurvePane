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
