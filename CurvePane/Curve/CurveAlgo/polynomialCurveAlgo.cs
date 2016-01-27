using System;
using System.Collections.Generic;
using System.Text;

using CurvePane.Curve.CurveParam;

namespace CurvePane.Curve.CurveAlgo
{
    public class polynomialCurveAlgo : ICurveAlgo
    {
        public void drawACurve(ICurveParam curveParam)
        {
            throw new NotImplementedException();
        }

        public bool canDrawCurve(ICurveParam curveParam)
        {
            //TODO: Test if any two of these points have the same X value.
            return true;
        }
    }
}
