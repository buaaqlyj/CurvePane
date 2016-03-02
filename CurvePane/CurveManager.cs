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
using System.Drawing;
using System.Windows.Forms;

using CurveBase.CurveData.CurveParam;
using CurveDraw.Curve;
using CurveDraw.Draw;
using CurvePane.ZedGraphTool;
using Util.Enum;
using Util.Variable;
using Util.Variable.PointList;
using ZedGraph;

namespace CurvePane
{
    public class CurveManager
    {
        private static ZedGraphWrapper zedGraph;

        private BaseDataPointList basePoints = null;
        private int baseNumber = 0;

        private Random random = null;

        private bool captureSwitch = false;
        private bool hasDrawBasePointsConnections = false;

        private List<string> curveNames = null;

        public static event ZedGraphWrapper.DataPointEventHandler AddBasePointEvent, DisplayBasePointEvent;
        public static event EventHandler BasePointChangedEvent;

        public CurveManager(ZedGraphControl zedGraphControl)
        {
            zedGraph = new ZedGraphWrapper(zedGraphControl, "BasePoints");
            basePoints = new BaseDataPointList();
            random = new Random();
            curveNames = new List<string>();
            ZedGraphWrapper.DoubleClick += new ZedGraphWrapper.DataPointEventHandler(ZedGraphWrapper_DoubleClick);
            ZedGraphWrapper.MouseMove += new ZedGraphWrapper.DataPointEventHandler(ZedGraphWrapper_MouseMove);
        }

        #region EventHandler
        public void ZedGraphWrapper_DoubleClick(Util.Variable.DataPoint point)
        {
            if (captureSwitch)
            {
                AddBasePoint(point);
            }
        }

        public void ZedGraphWrapper_MouseMove(Util.Variable.DataPoint point)
        {
            if (captureSwitch)
            {
                DisplayBasePointEvent(point);
            }
        }
        #endregion

        #region BasePoints Operation
        public void AddBasePoint(Util.Variable.DataPoint point)
        {
            foreach (Util.Variable.DataPoint item in basePoints)
            {
                if (item.Equals(point))
                {
                    MessageBox.Show("This point is very close to the other point!");
                    return;
                }
            }
            basePoints.Add(point);
            zedGraph.AddBasePoint(point);
            baseNumber++;
            AddBasePointEvent(point);
            BasePointChangedEvent(this, new EventArgs());
        }

        public void ClearBasePoint()
        {
            basePoints.Clear();
            zedGraph.ClearBasePoint();
            baseNumber = 0;
            BasePointChangedEvent(this, new EventArgs());
        }

        public void RemoveBasePoint(Util.Variable.DataPoint point)
        {
            basePoints.Remove(point);
            zedGraph.RemoveBasePoint(point);
            BasePointChangedEvent(this, new EventArgs());
        }

        public bool TryAddBasePointFromText(string text)
        {
            Util.Variable.DataPoint point;
            if (Util.Variable.DataPoint.TryParse(text, out point))
            {
                AddBasePoint(point);
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Util.Variable.DataPoint> getList()
        {
            return basePoints.Points;
        }

        public List<Util.Variable.DataPoint> getOrderedList()
        {
            return basePoints.SortedPointList;
        }
        #endregion

        #region Line Property
        private Color getNewColor()
        {
            return Color.FromArgb(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));
        }

        private string getNewName()
        {
            int i = 1;
            while (curveNames.Contains("Curve" + i.ToString()))
            {
                i++;
            }
            return "Curve" + i.ToString();
        }
        #endregion

        #region Draw
        public void DrawPolynomialCurve(string curveName, int polynomialType)
        {
            BaseDataPointList pointList = this.basePoints;
            PolynomialCurveParam curveParam = new PolynomialCurveParam(pointList.SortedPointList, (PolynomialCurveType)polynomialType);
            PolynomialCurve curve = new PolynomialCurve(curveParam);
            DrawLines(curveName, curve.sampleCurvePoints());
        }

        public void DrawCSICurve(string curveName, int borderConditionType, string leftVal, string rightVal)
        {
            BaseDataPointList pointList = this.basePoints;
            double left = 0, right = 0;
            if (borderConditionType < 3)
            {
                if (!Double.TryParse(leftVal, out left) || !Double.TryParse(rightVal, out right))
                {
                    throw new ArgumentException("The left or right border can't be recognized.");
                }
            }
            CubicSplineInterpolationCurveParam curveParam = new CubicSplineInterpolationCurveParam(pointList.SortedPointList, (CSIBorderConditionType)borderConditionType, new DoubleExtension(left), new DoubleExtension(right));
            CubicSplineInterpolationCurve curve = new CubicSplineInterpolationCurve(curveParam);
            DrawLines(curveName, curve.sampleCurvePoints());
        }

        public void DrawPCSICurve(string curveName, int borderConditionType, string val1, string val2, string val3, string val4)
        {
            BaseDataPointList pointList = this.basePoints;
            double subVal1 = 0, subVal2 = 0, subVal3 = 0, subVal4 = 0;
            if (borderConditionType != 2)
            {
                if (!Double.TryParse(val1, out subVal1) || !Double.TryParse(val2, out subVal2))
                {
                    throw new ArgumentException("The left or right border can't be recognized.");
                }
                if (borderConditionType == 3)
                {
                    if (!Double.TryParse(val3, out subVal3) || !Double.TryParse(val4, out subVal4))
                    {
                        throw new ArgumentException("The left or right border can't be recognized.");
                    }
                }
            }
            ParametricCubicSplineInterpolationCurveParam curveParam = new ParametricCubicSplineInterpolationCurveParam(pointList.Points, (PCSIBorderConditionType)borderConditionType, new DoubleExtension(subVal1), new DoubleExtension(subVal2), new DoubleExtension(subVal3), new DoubleExtension(subVal4));
            ParametricCubicSplineInterpolationCurve curve = new ParametricCubicSplineInterpolationCurve(curveParam);
            DrawLines(curveName, curve.sampleCurvePoints());
        }

        public void DrawBezierCurve(string curveName)
        {
            BaseDataPointList pointList = this.basePoints;
            BezierCurveParam curveParam = new BezierCurveParam(pointList.Points);
            BezierCurve curve = new BezierCurve(curveParam);
            DrawLines(curveName, curve.sampleCurvePoints());
        }

        public void DrawBSplineCurve(string curveName, string degree, string cutList)
        {
            BaseDataPointList pointList = this.basePoints;
            ArrayString aString = new ArrayString(cutList);
            List<DoubleExtension> cutPoints = null;
            if (!aString.tryParseDoubleExtension(out cutPoints))
            {
                throw new ArgumentException("The cutList contains unrecognised string: " + cutList, "cutList");
            }
            int degreeInt = 0;
            if (!Int32.TryParse(degree, out degreeInt))
            {
                throw new ArgumentException("The degree is not a integer string: " + degree, "degree");
            }
            if (degreeInt < 1)
            {
                throw new ArgumentException("The degree is invalid: " + degree, "degree");
            }
            BSplineCurveParam curveParam = new BSplineCurveParam(pointList.Points, degreeInt, cutPoints);
            BSplineCurve curve = new BSplineCurve(curveParam);
            DrawLines(curveName, curve.sampleCurvePoints());
        }

        public void DrawNURBSCurve(string curveName, string degree, string cutList, string weightList)
        {
            BaseDataPointList pointList = this.basePoints;
            ArrayString aString = new ArrayString(cutList);
            List<DoubleExtension> cutPoints = null, weightValues = null;
            if (!aString.tryParseDoubleExtension(out cutPoints))
            {
                throw new ArgumentException("The cutList contains unrecognised string: " + cutList, "cutList");
            }
            aString = new ArrayString(weightList);
            if (!aString.tryParseDoubleExtension(out weightValues))
            {
                throw new ArgumentException("The weightList contains unrecognised string: " + weightList, "weightList");
            }
            int degreeInt = 0;
            if (!Int32.TryParse(degree, out degreeInt))
            {
                throw new ArgumentException("The degree is not a integer string: " + degree, "degree");
            }
            if (degreeInt < 1)
            {
                throw new ArgumentException("The degree is invalid: " + degree, "degree");
            }
            NurbsCurveParam curveParam = new NurbsCurveParam(pointList.Points, degreeInt, cutPoints, weightValues);
            NurbsCurve curve = new NurbsCurve(curveParam);
            DrawLines(curveName, curve.sampleCurvePoints());
        }
        #endregion

        #region CurveName Operation
        private void AddCurveName(string curveName)
        {
            if (!curveNames.Contains(curveName))
            {
                curveNames.Add(curveName);
            }
        }

        private void ClearCurveName()
        {
            curveNames.Clear();
        }

        public bool ContainCurveName(string curveName)
        {
            if (curveName == "BasePoints") return true;
            else if (curveNames.Contains(curveName)) return true;
            else return false;
        }
        #endregion

        #region Lines Operation
        public void DrawLine(string curveName, PointPairList pointPairList, DrawType drawType, Color color)
        {
            if (!HasInitialized)
                throw new Exception("ZedGraphHelper hasn't been initialized!");
            switch (drawType)
            {
                case DrawType.DotNoLine:
                    zedGraph.AddDots(curveName, pointPairList, color);
                    break;
                case DrawType.DotLine:
                    zedGraph.AddLineWithDots(curveName, pointPairList, color);
                    break;
                case DrawType.LineNoDot:
                    zedGraph.AddLineWithoutDots(curveName, pointPairList, color);
                    break;
            }
        }

        public void DrawLines(string curveName, Dictionary<ICurvePointList, DrawType> interpolatedData)
        {
            Color color;
            foreach (KeyValuePair<ICurvePointList, DrawType> item in interpolatedData)
            {
                if (hasDrawBasePointsConnections)
                {
                    if (item.Key.PaneCurveType == PaneCurveType.connectingSupportingCurve) break;
                }
                else
                {
                    if (item.Key.PaneCurveType == PaneCurveType.connectingSupportingCurve) hasDrawBasePointsConnections = true;
                }
                color = getNewColor();
                DrawLine(curveName + item.Key.Label, ZedGraphWrapper.transformDataPointListToPointPairList(item.Key), item.Value, color);
            }
            AddCurveName(curveName);
        }

        public void RemoveAllLines()
        {
            zedGraph.RemoveAllLinesExceptCertainLine("BasePoints");
            hasDrawBasePointsConnections = false;
            ClearCurveName();
        }
        #endregion

        #region Property
        public bool HasInitialized
        {
            get
            {
                return zedGraph.HasInitialized;
            }
        }

        public bool CaptureSwitch
        {
            get
            {
                return captureSwitch;
            }
            set
            {
                captureSwitch = value;
            }
        }

        public int BasePointsCount
        {
            get
            {
                return basePoints.Count;
            }
        }

        public int BaseNumber
        {
            get
            {
                return baseNumber;
            }
        }

        public string NextAvailableName
        {
            get
            {
                return getNewName();
            }
        }
        #endregion

        #region Pane Operation
        public void UpdatePaneView()
        {
            zedGraph.UpdatePaneView();
        }
        #endregion

        #region Curve.Property
        #region B-Spline Curve
        public int GetMultiplycityOfNodes(string nodesString)
        {
            ArrayString aString = new ArrayString(nodesString);
            return aString.MaxCount;
        }
        #endregion
        #endregion
    }
}
