using System;
using System.Collections.Generic;
using System.Text;

using CurveBase.CurveElements.IntervalCurve;
using Util.Variable;

namespace CurveBase.CurveData.CurveInterpolatedData
{
    public class polynomialCurveInterpolatedData : ICurveInterpolatedData
    {
        private List<PolynomialCurve> polynomialInterpolatedCurves;

        #region Constructor

        public polynomialCurveInterpolatedData(List<PolynomialCurve> curves)
        {
            polynomialInterpolatedCurves = curves;
        }
        
        #endregion

        #region Property
        public List<PolynomialCurve> Curves
        {
            get
            {
                return polynomialInterpolatedCurves;
            }
        }
        #endregion

        #region ICurveInterpolatedData Member
        public CurveType getCurveType()
        {
            return CurveType.polynomialCurve;
        }

        public DataPoint getLastPoint()
        {
            return polynomialInterpolatedCurves[polynomialInterpolatedCurves.Count - 1].LastPoint;
        }
        #endregion


        
    }
}
