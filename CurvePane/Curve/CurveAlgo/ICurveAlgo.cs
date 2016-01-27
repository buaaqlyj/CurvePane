using System;
using System.Collections.Generic;
using System.Text;

using CurvePane.Curve.CurveParam;

namespace CurvePane.Curve.CurveAlgo
{
    public interface ICurveAlgo
    {
        void drawACurve(ICurveParam curveParam);

        bool canDrawCurve(ICurveParam curveParam);
    }
}
