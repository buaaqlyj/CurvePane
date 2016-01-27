using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CurvePane.Curve.CurveParam
{
    public enum CurveType
    {
        [Description("unknown curve type")]
        unknown = -1,
        [Description("polynomial curve type")]
        polynomialCurve = 1,
        [Description("cubic spline interpolation curve type")]
        csiCurve = 2,
        [Description("parametric cubic spline interpolation curve type")]
        pcsiCurve = 3,
        [Description("Bezier curve type")]
        bezierCurve = 4,
        [Description("B-Spline curve type")]
        bsCurve = 5,
        [Description("NURBS curve type")]
        nurbsCurve = 6
    }
}
