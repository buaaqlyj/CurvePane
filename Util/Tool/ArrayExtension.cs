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
using System.Text;

namespace Util.Tool
{
    public static class ArrayExtension
    {
        public static int GetMaxCountFromArray<T>(T[] array)
        {
            if (array.Length == 0)
            {
                return 0;
            }
            T lastOne = array[0];
            int maxCount = 1;
            int currentCount = 1;
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i].Equals(lastOne))
                {
                    currentCount++;
                }
                else
                {
                    lastOne = array[i];
                    if (currentCount > maxCount) maxCount = currentCount;
                    currentCount = 1;
                }
            }
            return maxCount;
        }

        public static int GetMaxCountFromArray<T>(List<T> list)
        {
            if (list.Count == 0)
            {
                return 0;
            }
            T lastOne = list[0];
            int maxCount = 1;
            int currentCount = 1;
            for (int i = 1; i < list.Count; i++)
            {
                if (list[i].Equals(lastOne))
                {
                    currentCount++;
                }
                else
                {
                    lastOne = list[i];
                    if (currentCount > maxCount) maxCount = currentCount;
                    currentCount = 1;
                }
            }
            return maxCount;
        }
    }
}
