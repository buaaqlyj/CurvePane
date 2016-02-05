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
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using CurveBase.CurveElements.IntervalCurve;
using CurveBase.CurveData.CurveInterpolatedData;
using Util.Variable;
using ZedGraph;

using CurveDraw.Curve;
using Util.Variable.PointList;

namespace CurvePane.ZedGraphTool
{
    public static class ZedGraphHelpler
    {
        private static GraphPane masterPane = null;
        private static BaseDataPointList basePoints = null;

        public static void Initialize(GraphPane masterPane)
        {
            ZedGraphHelpler.masterPane = masterPane;
            basePoints = new BaseDataPointList();
        }

        #region BasePoints

        #endregion

        #region Draw
        public static void DrawCurves(string curveName, Dictionary<PointPairList, DrawType> pointPairListData, Color color)
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

        public static LineItem DrawCurve(string curveName, PointPairList pointPairList, DrawType drawType, Color color)
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
        public static bool hasInitialized
        {
            get
            {
                return masterPane == null;
            }
        }
        
        public static GraphPane MasterPane
        {
            get
            {
                return masterPane;
            }
        }
        #endregion
    }
}
