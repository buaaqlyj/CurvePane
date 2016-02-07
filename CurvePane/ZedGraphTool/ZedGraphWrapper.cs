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

using System.Drawing;
using System.Windows.Forms;

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
