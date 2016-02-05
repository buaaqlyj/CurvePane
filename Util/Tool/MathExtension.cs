using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using Util.Variable;

namespace Util.Tool
{
    public static class MathExtension
    {
        public static List<double> calculateLinearPolynomialCoefficients(DataPoint pt1, DataPoint pt2)
        {
            Debug.Assert(pt1.X == pt2.X, "The two points have the same X value.");
            List<double> coefficients = new List<double>();
            double x1 = pt1.X.CoordinateValue;
            double y1 = pt1.Y.CoordinateValue;
            double x2 = pt2.X.CoordinateValue;
            double y2 = pt2.Y.CoordinateValue;
            double a = (y2 - y1) / (x2 - x1);
            double b = (y1 + y2 - a * (x1 + x2)) / 2;
            coefficients.Add(b);
            coefficients.Add(a);
            return coefficients;
        }

        public static List<double> calculateQuadraticPolynomialCoefficients(DataPoint pt1, DataPoint pt2, DataPoint pt3)
        {
            Debug.Assert(pt1.X == pt2.X, "Point1 and Point2 have the same X value.");
            Debug.Assert(pt1.X == pt3.X, "Point1 and Point3 have the same X value.");
            Debug.Assert(pt2.X == pt3.X, "Point2 and Point3 have the same X value.");
            List<double> coefficients = new List<double>();
            double x1 = pt1.X.CoordinateValue;
            double y1 = pt1.Y.CoordinateValue;
            double x2 = pt2.X.CoordinateValue;
            double y2 = pt2.Y.CoordinateValue;
            double x3 = pt3.X.CoordinateValue;
            double y3 = pt3.Y.CoordinateValue;
            double a = ((y3 - y2) / (x3 - x2) - (y3 - y1) / (x3 - x1)) / (x2 - x1);
            double b = (y3 - y2) / (x3 - x2) - (x2 + x3) * a;
            double c = y1 - x1 * b - x1 * x1 * a;
            coefficients.Add(c);
            coefficients.Add(b);
            coefficients.Add(a);
            return coefficients;
        }
    }
}
