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
using System.Text;

using CurveBase;
using CurveBase.CurveElements;
using CurveBase.CurveElements.IntervalCurve;
using CurveBase.CurveElements.PointList;
using CurveBase.CurveException;
using CurveBase.CurveData.CurveParamData;
using CurveBase.CurveData.CurveInterpolatedData;
using Util.Tool;
using Util.Variable;

namespace CurveDraw.Curve
{
    public class polynomialInterpolatedCurve : ICurve
    {
        #region ICurve Member

        public bool initial(ICurveParam curveParam)
        {
            throw new NotImplementedException();
        }

        public bool canDrawCurve(ICurveParam curveParam)
        {
            if (curveParam.getCurveType() == CurveType.polynomialCurve)
            {
                polynomialCurveParam param = (polynomialCurveParam)curveParam;
                if (!param.PointList.noDuplicatedX())
                    throw new SameXInOrderedCurvePointListException(CurveType.polynomialCurve);
            }
            else
            {
                throw new UnmatchedCurveParamTypeException(CurveType.polynomialCurve, curveParam.getCurveType());
            }
            return true;
        }

        public ICurveInterpolatedData interpolateCurve(ICurveParam curveParam)
        {
            try
            {
                canDrawCurve(curveParam);
            }
            catch (UnmatchedCurveParamTypeException ex)
            {
                //TODO: handle UnmatchedCurveParamTypeException
                return null;
            }
            catch (SameXInOrderedCurvePointListException ex)
            {
                //TODO: handle SameXInOrderedCurvePointListException
                return null;
            }

            polynomialCurveParam param = (polynomialCurveParam)curveParam;
            List<PolynomialCurve> polynomialCurves = generatePolynomialCurves(param);
        }

        public void drawCurve(ICurveInterpolatedData curveInterpolatedData)
        {
            curveInterpolatedData.drawCurve();
        }
        
        #endregion

        #region Private.Methods
        private List<PolynomialCurve> generatePolynomialCurves(polynomialCurveParam param)
        {
            polynomialCurveType curveType = param.PolynomialCurveType;
            OrderedCurvePointList pointList = param.PointList;
            List<PolynomialCurve> polynomialCurve = new List<PolynomialCurve>();
            List<double> coefficients;
            switch (curveType)
            {
                case polynomialCurveType.Lagrange_Linear:
                    for (int i = 1; i < pointList.Count; i++)
                    {
                        coefficients = MathExtension.calculateLinearPolynomialCoefficients(pointList[i - 1], pointList[i]);
                        polynomialCurve.Add(new PolynomialCurve(coefficients, 2, pointList[i - 1].X, pointList[i].X));
                    }
                    break;
                case polynomialCurveType.Lagrange_Quadratic:
                    //TODO: generate Polynomial Curves for Lagrange_Quadratic
                    break;
                case polynomialCurveType.Newton:
                    //TODO: generate Polynomial Curves for Newton
                    break;
            }
            return polynomialCurve;
        }
        #endregion

    }
}
