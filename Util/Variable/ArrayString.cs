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

using Util.Tool;

namespace Util.Variable
{
    public class ArrayString
    {
        private char[] chArray = null;
        private string[] strArray = null;
        private string originalString = "";

        #region Constructor
        public ArrayString(string originalString)
        {
            this.originalString = originalString;
            chArray = originalString.ToCharArray();
            strArray = originalString.Split(new char[] { ' ', ',' });
        }

        public ArrayString(string originalString, char[] splitChars)
        {
            this.originalString = originalString;
            chArray = originalString.ToCharArray();
            strArray = originalString.Split(splitChars);
        }

        public ArrayString(string[] subStrings, char separateChar)
        {
            if (subStrings.Length < 1)
            {
                originalString = "";
                chArray = new char[] { };
                strArray = new string[] { };
            }
            else
            {
                originalString = subStrings[0];
                if (subStrings.Length == 1)
                {
                    strArray = new string[] { originalString };
                    chArray = originalString.ToCharArray();
                }
                else
                {
                    subStrings.CopyTo(strArray, 0);
                    for (int i = 1; i < subStrings.Length; i++)
                    {
                        originalString += subStrings[i] + separateChar.ToString();
                    }
                    chArray = originalString.ToCharArray();
                }
            }
        }
        #endregion

        #region Property
        public int Count
        {
            get
            {
                return strArray.Length;
            }
        }

        public int Length
        {
            get
            {
                return chArray.Length;
            }
        }

        public string[] StringArray
        {
            get
            {
                return strArray;
            }
        }

        public char[] CharArray
        {
            get
            {
                return chArray;
            }
        }

        public int MaxCount
        {
            get
            {
                return ArrayExtension.GetMaxCountFromArray<string>(StringArray);
            }
        }
        #endregion

        #region Public Member
        public bool TryParseDoubleExtension(out List<DoubleExtension> doubleExtensionList)
        {
            double val;
            doubleExtensionList = new List<DoubleExtension>();
            foreach (string item in strArray)
            {
                if (Double.TryParse(item, out val))
                {
                    doubleExtensionList.Add(new DoubleExtension(val));
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
    }
}
