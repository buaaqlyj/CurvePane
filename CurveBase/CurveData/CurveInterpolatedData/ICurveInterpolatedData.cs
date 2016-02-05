using System;
using System.Collections.Generic;
using System.Text;

using Util.Variable;

namespace CurveBase.CurveData.CurveInterpolatedData
{
    public interface ICurveInterpolatedData
    {
        CurveType getCurveType();

        DataPoint getLastPoint();
    }
}
