using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using CurveDraw.Curve;
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

        public ZedGraphWrapper(ZedGraphControl zedGraphControl, string baseName)
        {
            this.zedGraphControl = zedGraphControl;
            this.masterPane = zedGraphControl.GraphPane;
            baseLine = AddDots(baseName, new PointPairList(), Color.Purple);
            zedGraphControl.DoubleClickEvent += new ZedGraphControl.ZedMouseEventHandler(zedGraphControl_DoubleClickEvent);
        }

        #region EventHandler
        public delegate void DoubleClickEventHandler(Util.Variable.DataPoint point);
        public static event DoubleClickEventHandler DoubleClick;
        public bool zedGraphControl_DoubleClickEvent(object sender, MouseEventArgs e)
        {
            double xVal, yVal;
            masterPane.ReverseTransform(e.Location, out xVal, out yVal);
            DoubleClick(new Util.Variable.DataPoint(xVal, yVal));
            return true;
        }
        #endregion

        #region BasePoints Operation
        public void AddBasePoint(Util.Variable.DataPoint point)
        {
            baseLine.AddPoint(transformDataPointToPointPair(point));
            zedGraphControl.Refresh();
        }

        public void ClearBasePoint()
        {
            baseLine.Clear();
            zedGraphControl.Refresh();
        }

        public void RemoveBasePoint(Util.Variable.DataPoint point)
        {
            Util.Variable.DataPoint localPoint;
            for (int i = 0; i < baseLine.Points.Count; i++ )
            {
                localPoint = new Util.Variable.DataPoint(baseLine.Points[i].X, baseLine.Points[i].Y);
                if (localPoint == point)
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
            zedGraphControl.Refresh();
            return line;
        }

        public LineItem AddLineWithoutDots(string curveName, PointPairList pointPairList, Color color)
        {
            LineItem line = masterPane.AddCurve(curveName, pointPairList, color, SymbolType.None);
            zedGraphControl.Refresh();
            return line;
        }

        public LineItem AddLineWithDots(string curveName, PointPairList pointPairList, Color color)
        {
            LineItem line = masterPane.AddCurve(curveName, pointPairList, color, SymbolType.XCross);
            zedGraphControl.Refresh();
            return line;
        }

        public void RemoveLine(string lineName)
        {
            masterPane.CurveList.Remove(masterPane.CurveList[masterPane.CurveList.IndexOf(lineName)]);
            zedGraphControl.Refresh();
        }

        public void RemoveLines(string keyword)
        {
            masterPane.CurveList.RemoveAll((c) => { return c.Label.Text.Contains(keyword); });
            zedGraphControl.Refresh();
        }

        public void RemoveAllLinesExceptCertainLine(string lineName)
        {
            masterPane.CurveList.RemoveAll((c) => { return c.Label.Text != lineName; });
            zedGraphControl.Refresh();
        }

        public void RemoveAllLines()
        {
            masterPane.CurveList.Clear();
            zedGraphControl.Refresh();
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

        public GraphPane MasterPane
        {
            get
            {
                return masterPane;
            }
        }
        #endregion

        #region Public.Interface
        public static PointPair transformDataPointToPointPair(Util.Variable.DataPoint point)
        {
            return new PointPair(point.X.CoordinateValue, point.Y.CoordinateValue);
        }

        public static PointPairList transformDataPointListToPointPairList(ICurvePointList points)
        {
            PointPairList list = new PointPairList();
            foreach (Util.Variable.DataPoint item in points)
            {
                list.Add(transformDataPointToPointPair(item));
            }
            return list;
        }
        #endregion
    }
}
