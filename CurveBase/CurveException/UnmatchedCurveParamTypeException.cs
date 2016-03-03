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
using System.Runtime.Serialization;

using Util.Tool;

namespace CurveBase.CurveException
{
    [Serializable]
    public sealed class UnmatchedCurveParamTypeException : Exception, ISerializable
    {
        private InterpolationCurveType curveType1 = InterpolationCurveType.unknown, curveType2 = InterpolationCurveType.unknown;
        
        #region Constructor
        public UnmatchedCurveParamTypeException()
            : base() { }

        public UnmatchedCurveParamTypeException(string message)
            : base(message) { }

        public UnmatchedCurveParamTypeException(string message, Exception innerException)
            : base(message, innerException) { }

        public UnmatchedCurveParamTypeException(InterpolationCurveType curveType1, InterpolationCurveType curveType2)
            : base() 
        {
            this.curveType1 = curveType1;
            this.curveType2 = curveType2;
        }

        public UnmatchedCurveParamTypeException(string message, InterpolationCurveType curveType1, InterpolationCurveType curveType2)
            : base(message) 
        {
            this.curveType1 = curveType1;
            this.curveType2 = curveType2;
        }

        public UnmatchedCurveParamTypeException(string message, InterpolationCurveType curveType1, InterpolationCurveType curveType2, Exception innerException)
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
                if (curveType1 != InterpolationCurveType.unknown && curveType2 != InterpolationCurveType.unknown)
                    msg = "The curve you are drawing is " + EnumExtension.GetDescriptionFromEnumValue<InterpolationCurveType>(curveType1) + ", but you used the parameters of " + EnumExtension.GetDescriptionFromEnumValue<InterpolationCurveType>(curveType2) + ".";
                return msg;
            }
        }
        #endregion

        #region ISerializable Member
        private UnmatchedCurveParamTypeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            curveType1 = (InterpolationCurveType)info.GetInt32("DrawingType");
            curveType2 = (InterpolationCurveType)info.GetInt32("ParameterType");
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("DrawingType", (int)curveType1);
            info.AddValue("ParameterType", (int)curveType2);
            base.GetObjectData(info, context);
        }
        #endregion

        #region Property
        public InterpolationCurveType DrawingType
        {
            get
            {
                return curveType1;
            }
        }

        public InterpolationCurveType ParameterType
        {
            get
            {
                return curveType2;
            }
        }
        #endregion
    }
}
