using System;
using System.Collections.Generic;
using System.Text;

using CurveBase.CurveData.CurveParamData;
using CurveBase.CurveElements.IntervalCurve;
using Util.Variable;
using Util.Variable.PointList;

namespace CurveBase.CurveData.CurveInterpolatedData
{
    public class polynomialCurveInterpolatedData : ICurveInterpolatedData
    {
        private List<PolynomialCurve> polynomialInterpolatedCurves;
        private OrderedCurvePointList polynomialInterpolatedPoints;
        private polynomialCurveType curveType;

        #region Constructor

        public polynomialCurveInterpolatedData(List<PolynomialCurve> curves, polynomialCurveType curveType)
        {
            polynomialInterpolatedCurves = curves;
            polynomialInterpolatedPoints = null;
            this.curveType = curveType;
        }

        public polynomialCurveInterpolatedData(OrderedCurvePointList points, polynomialCurveType curveType)
        {
            polynomialInterpolatedCurves = null;
            polynomialInterpolatedPoints = points;
            this.curveType = curveType;
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

        public OrderedCurvePointList PointList
        {
            get
            {
                return polynomialInterpolatedPoints;
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
