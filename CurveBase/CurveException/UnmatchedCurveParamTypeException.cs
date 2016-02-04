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
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

using CurveBase.CurveData.CurveParamData;
using Util.Tool;

namespace CurveBase.CurveException
{
    [Serializable]
    public sealed class UnmatchedCurveParamTypeException : Exception, ISerializable
    {
        private CurveType curveType1 = CurveType.unknown, curveType2 = CurveType.unknown;
        
        #region Constructor
        public UnmatchedCurveParamTypeException()
            : base() { }

        public UnmatchedCurveParamTypeException(string message)
            : base(message) { }

        public UnmatchedCurveParamTypeException(string message, Exception innerException)
            : base(message, innerException) { }

        public UnmatchedCurveParamTypeException(CurveType curveType1, CurveType curveType2)
            : base() 
        {
            this.curveType1 = curveType1;
            this.curveType2 = curveType2;
        }

        public UnmatchedCurveParamTypeException(string message, CurveType curveType1, CurveType curveType2)
            : base(message) 
        {
            this.curveType1 = curveType1;
            this.curveType2 = curveType2;
        }

        public UnmatchedCurveParamTypeException(string message, CurveType curveType1, CurveType curveType2, Exception innerException)
            : base(message, innerException) 
        {
            this.curveType1 = curveType1;
            this.curveType2 = curveType2;
        }
        #endregion

        #region Exception Member
        public override string Message
        {
            get
            {
                string msg = base.Message;
                if (curveType1 != CurveType.unknown && curveType2 != CurveType.unknown)
                    msg += Environment.NewLine + "The curve you are drawing is " + EnumExtension.GetDescriptionFromValue<CurveType>(curveType1) + ", but you used the parameters of " + EnumExtension.GetDescriptionFromValue<CurveType>(curveType2) + ".";
                return msg;
            }
        }
        #endregion

        #region ISerializable Member
        private UnmatchedCurveParamTypeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            curveType1 = (CurveType)info.GetInt32("DrawingType");
            curveType2 = (CurveType)info.GetInt32("ParameterType");
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("DrawingType", (int)curveType1);
            info.AddValue("ParameterType", (int)curveType2);
            base.GetObjectData(info, context);
        }
        #endregion

        #region Property
        public CurveType DrawingType
        {
            get
            {
                return curveType1;
            }
        }

        public CurveType ParameterType
        {
            get
            {
                return curveType2;
            }
        }
        #endregion
    }
}
