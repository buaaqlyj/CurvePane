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

using System.Collections.Generic;

using Util.Enum;

namespace Util.Variable.PointList
{
    public interface ICurvePointList
    {
        int IndexOf(DataPoint item);

        void RemoveAt(int index);

        DataPoint this[int index] { get; set; }

        void Add(DataPoint item);

        void Clear();

        bool Contains(DataPoint item);

        void CopyTo(DataPoint[] array, int arrayIndex);

        int Count { get; }

        bool Remove(DataPoint item);

        IEnumerator<DataPoint> GetEnumerator();

        string Label { get; set; }

        PaneCurveType PaneCurveType { get; set; }

        List<DoubleExtension> XList { get; }

        List<DoubleExtension> YList { get; }
    }
}