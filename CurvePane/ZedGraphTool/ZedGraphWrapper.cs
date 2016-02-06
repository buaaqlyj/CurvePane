using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using CurveDraw.Curve;
using Util.Variable.PointList;
using ZedGraph;

namespace CurvePane.ZedGraphTool
{
    public class ZedGraphWrapper
    {
        private ZedGraphControl zedGraphControl = null;
        private GraphPane masterPane = null;

        private BaseDataPointList basePoints = null;
        private LineItem baseLine = null;

        public ZedGraphWrapper(ZedGraphControl zedGraphControl)
        {
            this.zedGraphControl = zedGraphControl;
            this.masterPane = zedGraphControl.GraphPane;
            basePoints = new BaseDataPointList();
            baseLine = DrawCurve("BaseLine", new PointPairList(), DrawType.DotNoLine, Color.Purple);
        }


        #region BasePoints
        public void AddBasePoint(Util.Variable.DataPoint point)
        {
            basePoints.Add(point);
            baseLine.AddPoint(transformDataPointToPointPair(point));
            zedGraphControl.Refresh();
        }

        public void ClearBasePoint()
        {
            basePoints.Clear();
            baseLine.Clear();
            zedGraphControl.Refresh();
        }

        public void RemoveBasePointAt(int index)
        {
            basePoints.RemoveAt(index);
            baseLine.RemovePoint(index);
            zedGraphControl.Refresh();
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

        #region Draw
        public void DrawCurves(string curveName, Dictionary<PointPairList, DrawType> pointPairListData, Color color)
        {
            //TODO: Draw curves.
            List<LineItem> lines = new List<LineItem>();
            LineItem line;
            int index = 1;
            foreach (KeyValuePair<PointPairList, DrawType> item in pointPairListData)
            {
                line = DrawCurve(curveName + index.ToString(), item.Key, item.Value, color);
                if (line != null)
                {
                    lines.Add(line);
                }
                index++;
            }
        }

        public LineItem DrawCurve(string curveName, PointPairList pointPairList, DrawType drawType, Color color)
        {
            if (!hasInitialized)
                throw new Exception("ZedGraphHelper hasn't been initialized!");
            switch (drawType)
            {
                case DrawType.DotNoLine:
                    LineItem line = masterPane.AddCurve(curveName, pointPairList, color, SymbolType.XCross);
                    line.Line.IsVisible = false;
                    return line;
                case DrawType.DotLine:
                    return masterPane.AddCurve(curveName, pointPairList, color, SymbolType.XCross);
                case DrawType.LineNoDot:
                    return masterPane.AddCurve(curveName, pointPairList, color, SymbolType.None);
            }
            return null;
        }
        #endregion

        #region Property
        public bool hasInitialized
        {
            get
            {
                return zedGraphControl == null;
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

        public static PointPairList transformDataPointListToPointPairList(List<Util.Variable.DataPoint> points)
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
