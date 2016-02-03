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
using System.ComponentModel;
using System.Text;

namespace CurvePane.Curve.CurveParam
{
    public enum CurveType
    {
        [Description("unknown curve type")]
        unknown = -1,
        [Description("polynomial curve type")]
        polynomialCurve = 1,
        [Description("cubic spline interpolation curve type")]
        csiCurve = 2,
        [Description("parametric cubic spline interpolation curve type")]
        pcsiCurve = 3,
        [Description("Bezier curve type")]
        bezierCurve = 4,
        [Description("B-Spline curve type")]
        bsCurve = 5,
        [Description("NURBS curve type")]
        nurbsCurve = 6
    }
}
