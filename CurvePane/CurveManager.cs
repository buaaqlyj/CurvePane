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

using CurveBase.CurveData.CurveParamData;
using CurveBase.CurveException;
using CurveDraw.Curve;
using CurveDraw.Draw;
using CurvePane.ZedGraphTool;
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

        private List<string> curveNames = null;

        public static event ZedGraphWrapper.DataPointEventHandler AddBasePointEvent, DisplayBasePointEvent;
        
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
        }

        public void ClearBasePoint()
        {
            basePoints.Clear();
            zedGraph.ClearBasePoint();
            baseNumber = 0;
        }

        public void RemoveBasePoint(Util.Variable.DataPoint point)
        {
            basePoints.Remove(point);
            zedGraph.RemoveBasePoint(point);
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
            polynomialCurveParam curveParam = new polynomialCurveParam(pointList.SortedPointList, (polynomialCurveType)polynomialType);
            polynomialCurve curve = new polynomialCurve(curveParam);
            Dictionary<ICurvePointList, DrawType> interpolatedData = curve.sampleCurvePoints();
            Color color;
            foreach (KeyValuePair<ICurvePointList, DrawType> item in interpolatedData)
            {
                color = getNewColor();
                DrawLine(curveName + item.Key.Label, ZedGraphWrapper.transformDataPointListToPointPairList(item.Key), item.Value, color);
            }
            AddCurveName(curveName);
        }

        public void DrawBezierCurve(string curveName)
        {
            BaseDataPointList pointList = this.basePoints;
            bezierCurveParam curveParam = new bezierCurveParam(pointList.Points);
            bezierCurve curve = new bezierCurve(curveParam);
            Dictionary<ICurvePointList, DrawType> interpolatedData = curve.sampleCurvePoints();
            Color color;
            foreach (KeyValuePair<ICurvePointList, DrawType> item in interpolatedData)
            {
                color = getNewColor();
                DrawLine(curveName + item.Key.Label, ZedGraphWrapper.transformDataPointListToPointPairList(item.Key), item.Value, color);
            }
            AddCurveName(curveName);
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

        public void RemoveAllLines()
        {
            zedGraph.RemoveAllLinesExceptCertainLine("BasePoints");
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

    }
}
