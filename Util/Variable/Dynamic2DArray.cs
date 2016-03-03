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

namespace Util.Variable
{
    public class Dynamic2DArray<T>
    {
        private List<List<T>> array;

        #region Constructor
        public Dynamic2DArray()
        {
            array = new List<List<T>>();
        }
        #endregion

        #region Property
        public List<T> this[int index]
        {
            get
            {
                if (index < RowCount)
                    return array[index];
                return new List<T>();
            }
        }

        public int RowCount
        {
            get
            {
                return array.Count;
            }
        }
        #endregion

        #region Public Member
        public bool HasValue(int index1, int index2)
        {
            if (index1 < RowCount)
            {
                if (GetRowLength(index1) > index2)
                {
                    return true;
                }
            }
            return false;
        }

        public T GetArrayElement(int index1, int index2)
        {
            if (HasValue(index1, index2)) return array[index1][index2];
            throw new ArgumentException("The indexes are out of range! X: " + index1.ToString() + ", Y: " + index2.ToString());
        }

        public void SetArrayElement(int index1, int index2, T val)
        {
            if (index1 >= RowCount)
            {
                for (int i = RowCount; i <= index1; i++)
                {
                    array.Add(new List<T>());
                }
            }
            if (GetRowLength(index1) <= index2)
            {
                for (int i = GetRowLength(index1); i <= index2; i++)
                {
                    array[index1].Add(default(T));
                }
            }
            array[index1][index2] = val;
        }

        public int GetRowLength(int index)
        {
            return array[index].Count;
        }
        #endregion
    }
}
