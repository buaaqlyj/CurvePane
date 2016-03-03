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
using System.Drawing;
using System.Windows.Forms;

using Util.Tool;
using Util.Variable;
using Util.Variable.PointList;
using ZedGraph;

namespace CurvePane.ZedGraphTool
{
    public class ZedGraphWrapper
    {
        private ZedGraphControl zedGraphControl = null;
        private GraphPane masterPane = null;
        private LineItem baseLine = null;

        private bool sameStepForXY = false;

        private double heightMultiplier = 1;

        public ZedGraphWrapper(ZedGraphControl zedGraphControl, string baseName)
        {
            this.zedGraphControl = zedGraphControl;
            this.masterPane = zedGraphControl.GraphPane;
            baseLine = AddDots(baseName, new PointPairList(), Color.Purple);

            zedGraphControl.DoubleClickEvent += new ZedGraphControl.ZedMouseEventHandler(zedGraphControl_DoubleClickEvent);
            zedGraphControl.MouseMoveEvent += new ZedGraphControl.ZedMouseEventHandler(zedGraphControl_MouseMoveEvent);
            zedGraphControl.GraphPane.AxisChangeEvent += new GraphPane.AxisChangeEventHandler(GraphPane_AxisChangeEvent);

            PointF paneLeftUpper = this.masterPane.GeneralTransform(new PointF(0, 0), CoordType.ChartFraction);
            PointF paneRightLower = this.masterPane.GeneralTransform(new PointF(1, 1), CoordType.ChartFraction);

            heightMultiplier = (paneRightLower.X - paneLeftUpper.X) / (paneRightLower.Y - paneLeftUpper.Y);
        }

        #region EventHandler
        public delegate void DataPointEventHandler(Util.Variable.DataPoint point);
        public static event DataPointEventHandler DoubleClick;
        public static event DataPointEventHandler MouseMove;

        public bool zedGraphControl_DoubleClickEvent(object sender, MouseEventArgs e)
        {
            double xVal, yVal;
            masterPane.ReverseTransform(e.Location, out xVal, out yVal);
            DoubleClick(new Util.Variable.DataPoint(xVal, yVal));
            return false;
        }

        public bool zedGraphControl_MouseMoveEvent(object sender, MouseEventArgs e)
        {
            double xVal, yVal;
            masterPane.ReverseTransform(e.Location, out xVal, out yVal);
            MouseMove(new Util.Variable.DataPoint(xVal, yVal));
            return false;
        }

        public void GraphPane_AxisChangeEvent(GraphPane pane)
        {
            if (sameStepForXY)
            {
                double realHeight = (zedGraphControl.GraphPane.YAxis.Scale.Max - zedGraphControl.GraphPane.YAxis.Scale.Min) * heightMultiplier;
                double realWidth = zedGraphControl.GraphPane.XAxis.Scale.Max - zedGraphControl.GraphPane.XAxis.Scale.Min;
                DoubleExtension multiplier, smallVal, bigVal;
                if (realHeight > realWidth)
                {
                    multiplier = new DoubleExtension(realHeight / realWidth);
                    MathExtension.MiddleBasedResize(new DoubleExtension(zedGraphControl.GraphPane.XAxis.Scale.Min), new DoubleExtension(zedGraphControl.GraphPane.XAxis.Scale.Max), multiplier, out smallVal, out bigVal);
                    zedGraphControl.GraphPane.XAxis.Scale.Min = smallVal.AccurateValue;
                    zedGraphControl.GraphPane.XAxis.Scale.Max = bigVal.AccurateValue;
                    zedGraphControl.GraphPane.XAxis.Scale.MajorStep = MathExtension.DynamicRound((bigVal.AccurateValue - smallVal.AccurateValue) / 6);
                    zedGraphControl.GraphPane.XAxis.Scale.MinorStep = MathExtension.DynamicRound((bigVal.AccurateValue - smallVal.AccurateValue) / 6) / 4;
                }
                else
                {
                    multiplier = new DoubleExtension(realWidth / realHeight);
                    MathExtension.MiddleBasedResize(new DoubleExtension(zedGraphControl.GraphPane.YAxis.Scale.Min), new DoubleExtension(zedGraphControl.GraphPane.YAxis.Scale.Max), multiplier, out smallVal, out bigVal);
                    zedGraphControl.GraphPane.YAxis.Scale.Min = smallVal.AccurateValue;
                    zedGraphControl.GraphPane.YAxis.Scale.Max = bigVal.AccurateValue;
                    zedGraphControl.GraphPane.YAxis.Scale.MajorStep = MathExtension.DynamicRound((bigVal.AccurateValue - smallVal.AccurateValue) / 6);
                    zedGraphControl.GraphPane.YAxis.Scale.MinorStep = MathExtension.DynamicRound((bigVal.AccurateValue - smallVal.AccurateValue) / 6) / 4;
                }
            }
        }
        #endregion

        #region BasePoints Operation
        public void AddBasePoint(Util.Variable.DataPoint point)
        {
            baseLine.AddPoint(TransformDataPointToPointPair(point));
            zedGraphControl.Refresh();
        }

        public void ClearBasePoint()
        {
            baseLine.Clear();
            zedGraphControl.Refresh();
        }

        public void RemoveBasePoint(Util.Variable.DataPoint point)
        {
            for (int i = 0; i < baseLine.Points.Count; i++ )
            {
                if (point.Equals(new Util.Variable.DataPoint(baseLine.Points[i].X, baseLine.Points[i].Y)))
                {
                    baseLine.RemovePoint(i);
                    zedGraphControl.Refresh();
                    return;
                }
            }
        }
        #endregion
        
        #region Line Operation
        public LineItem AddDots(string curveName, PointPairList pointPairList, Color color)
        {
            LineItem line = masterPane.AddCurve(curveName, pointPairList, color, SymbolType.XCross);
            line.Line.IsVisible = false;
            UpdatePaneView();
            return line;
        }

        public LineItem AddLineWithoutDots(string curveName, PointPairList pointPairList, Color color)
        {
            LineItem line = masterPane.AddCurve(curveName, pointPairList, color, SymbolType.None);
            UpdatePaneView();
            return line;
        }

        public LineItem AddLineWithDots(string curveName, PointPairList pointPairList, Color color)
        {
            LineItem line = masterPane.AddCurve(curveName, pointPairList, color, SymbolType.XCross);
            UpdatePaneView();
            return line;
        }

        public void RemoveLine(string lineName)
        {
            masterPane.CurveList.Remove(masterPane.CurveList[masterPane.CurveList.IndexOf(lineName)]);
            UpdatePaneView();
        }

        public void RemoveLines(string keyword)
        {
            masterPane.CurveList.RemoveAll((c) => { return c.Label.Text.Contains(keyword); });
            UpdatePaneView();
        }

        public void RemoveAllLinesExceptCertainLine(string lineName)
        {
            masterPane.CurveList.RemoveAll((c) => { return c.Label.Text != lineName; });
            UpdatePaneView();
        }

        public void RemoveAllLines()
        {
            masterPane.CurveList.Clear();
            UpdatePaneView();
        }
        #endregion

        #region Pane Operation
        public void UpdatePaneView()
        {
            zedGraphControl.GraphPane.AxisChange();
            zedGraphControl.Refresh();
        }

        public void RestorePaneScale()
        {
            zedGraphControl.RestoreScale(zedGraphControl.GraphPane);
        }
        #endregion

        #region Property
        public bool HasInitialized
        {
            get
            {
                return zedGraphControl != null;
            }
        }

        public bool SameStepForXY
        {
            get
            {
                return sameStepForXY;
            }
            set
            {
                sameStepForXY = value;
            }
        }
        #endregion

        #region Public Member
        public Util.Variable.DataPoint TransformFromPaneToScreen(Util.Variable.DataPoint panePoint)
        {
            PointF pt = this.masterPane.GeneralTransform(new PointF((float)panePoint.X.AccurateValue, (float)panePoint.Y.AccurateValue), CoordType.AxisXYScale);
            return new Util.Variable.DataPoint(pt.X, pt.Y);
        }

        public Util.Variable.DataPoint TransformFromScreenToPane(Util.Variable.DataPoint screenPoint)
        {
            double xVal, yVal;
            this.masterPane.ReverseTransform(new PointF((float)screenPoint.X.AccurateValue, (float)screenPoint.Y.AccurateValue), out xVal, out yVal);
            return new Util.Variable.DataPoint(xVal, yVal);
        }
        #endregion

        #region Class Member
        public static PointPair TransformDataPointToPointPair(Util.Variable.DataPoint point)
        {
            return new PointPair(point.X.AccurateValue, point.Y.AccurateValue);
        }

        public static PointPairList TransformDataPointListToPointPairList(ICurvePointList points)
        {
            PointPairList list = new PointPairList();
            foreach (Util.Variable.DataPoint item in points)
            {
                list.Add(TransformDataPointToPointPair(item));
            }
            return list;
        }
        #endregion
    }
}
