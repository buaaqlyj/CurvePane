using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CurveDraw.Curve
{
    public enum DrawType
    {
        [Description("This PointList won't be drawn!")]
        None = 0,
        [Description("Only dots of this PointList will be drawn!")]
        DotNoLine = 1,
        [Description("Dots and lines of this PointList will be both drawn!")]
        DotLine = 2,
        [Description("Only lines of this PointList will be drawn!")]
        LineNoDot = 3
    }
}
