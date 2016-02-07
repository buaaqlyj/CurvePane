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

using System.ComponentModel;

namespace CurveDraw.Draw
{
    public enum DrawType
    {
        [Description("This PointList won't be drawn!")]
        None = 0,
        [Description("Only dots of this PointList will be drawn!")]
        DotNoLine = 1,
        [Description("Dots and lines of this PointList will be both drawn!")]
        DotLine = 2,
        [Description("Only lines of this PointList will be drawn!")]
        LineNoDot = 3
    }
}
