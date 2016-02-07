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

using CurveBase.CurveData.CurveParamData;
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

        public static event ZedGraphWrapper.DoubleClickEventHandler AddBasePointEvent;
        
        public CurveManager(ZedGraphControl zedGraphControl)
        {
            zedGraph = new ZedGraphWrapper(zedGraphControl, "BaseLine");
            basePoints = new BaseDataPointList();
            random = new Random();
            ZedGraphWrapper.DoubleClick += new ZedGraphWrapper.DoubleClickEventHandler(ZedGraphWrapper_DoubleClick);
        }

        #region EventHandler
        public void ZedGraphWrapper_DoubleClick(Util.Variable.DataPoint point)
        {
            if (captureSwitch)
            {
                AddBasePoint(point);
                AddBasePointEvent(point);
            }
        }
        #endregion

        #region BasePoints Operation
        public void AddBasePoint(Util.Variable.DataPoint point)
        {
            basePoints.Add(point);
            zedGraph.AddBasePoint(point);
            baseNumber++;
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

        #region Color
        private Color getNewColor()
        {
            return Color.FromArgb(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));
        }
        #endregion

        #region Draw
        public void DrawPolynomialCurve(string curveName, int polynomialType)
        {
            BaseDataPointList pointList = this.basePoints;
            polynomialCurveParam curveParam = new polynomialCurveParam(pointList.SortedPointList, (polynomialCurveType)polynomialType);
            polynomialCurve curve = new polynomialCurve(curveParam);
            Dictionary<ICurvePointList, DrawType> interpolatedData = curve.sampleCurvePoints();
            Color color = getNewColor();
            foreach (KeyValuePair<ICurvePointList, DrawType> item in interpolatedData)
            {
                DrawLine(curveName + item.Key.Label, ZedGraphWrapper.transformDataPointListToPointPairList(item.Key), item.Value, color);
            }
        }
        #endregion

        #region Lines Operation

        public LineItem DrawLine(string curveName, PointPairList pointPairList, DrawType drawType, Color color)
        {
            if (!HasInitialized)
                throw new Exception("ZedGraphHelper hasn't been initialized!");
            switch (drawType)
            {
                case DrawType.DotNoLine:
                    return zedGraph.AddDots(curveName, pointPairList, color);
                case DrawType.DotLine:
                    return zedGraph.AddLineWithDots(curveName, pointPairList, color);
                case DrawType.LineNoDot:
                    return zedGraph.AddLineWithoutDots(curveName, pointPairList, color);
            }
            return null;
        }

        public void DrawLines(string curveName, Dictionary<PointPairList, DrawType> pointPairListData, Color color)
        {
            //TODO: Draw curves.
            List<LineItem> lines = new List<LineItem>();
            LineItem line;
            int index = 1;
            foreach (KeyValuePair<PointPairList, DrawType> item in pointPairListData)
            {
                line = DrawLine(curveName + index.ToString(), item.Key, item.Value, color);
                if (line != null)
                {
                    lines.Add(line);
                }
                index++;
            }
        }

        public void RemoveAllLines()
        {
            zedGraph.RemoveAllLinesExceptCertainLine("BaseLine");
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
        #endregion

    }
}
