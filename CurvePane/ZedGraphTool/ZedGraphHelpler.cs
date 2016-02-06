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
    public class ZedGraphHelpler
    {
        private static ZedGraphWrapper zedGraph;

        public static void Initialize(ZedGraphControl zedGraphControl)
        {
            zedGraph = new ZedGraphWrapper(zedGraphControl);
            
        }

        #region BasePoints
        public void AddBasePoint(Util.Variable.DataPoint point)
        {
            zedGraph.AddBasePoint(point);
        }

        public void ClearBasePoint()
        {
            zedGraph.ClearBasePoint();
        }

        public void RemoveBasePointAt(int index)
        {
            zedGraph.RemoveBasePointAt(index);
        }

        public List<Util.Variable.DataPoint> getList()
        {
            return zedGraph.getList();
        }

        public List<Util.Variable.DataPoint> getOrderedList()
        {
            return zedGraph.getOrderedList();
        }
        #endregion

        #region Property
        
        #endregion

    }
}
