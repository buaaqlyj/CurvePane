﻿/// Copyright 2016 Troy Lewis. Some Rights Reserved
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

using CurvePane.Curve.CurveElements;
using CurvePane.Curve.CurveElements.IntervalCurve;
using CurvePane.Curve.CurveElements.Point;
using CurvePane.Curve.CurveElements.PointList;
using CurvePane.Curve.CurveParam;
using CurvePane.Extension.Tool;
using CurvePane.UserExceptions;
using CurvePane.ZedGraphTool;

namespace CurvePane.Curve.CurveAlgo
{
    public class polynomialCurveAlgo : ICurveAlgo
    {
        #region ICurveAlgo Member
        public void drawACurve(ICurveParam curveParam)
        {
            try
            {
                canDrawCurve(curveParam);
            }
            catch (UnmatchedCurveParamTypeException ex)
            {
                //TODO: handle UnmatchedCurveParamTypeException
                return;
            }
            catch (SameXInOrderedCurvePointListException ex)
            {
                //TODO: handle SameXInOrderedCurvePointListException
                return;
            }

            polynomialCurveParam param = (polynomialCurveParam)curveParam;
            List<PolynomialCurve> polynomialCurves = generatePolynomialCurves(param);

            ZedGraphHelpler.DrawPolynomialCurves(polynomialCurves);
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
