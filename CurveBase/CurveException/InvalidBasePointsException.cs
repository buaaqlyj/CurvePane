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
    public sealed class InvalidBasePointsException : Exception, ISerializable
    {
        private InterpolationCurveType curveType = InterpolationCurveType.unknown;
        private string additionalMessage = "";

        #region Constructor
        public InvalidBasePointsException()
            : base() { }

        public InvalidBasePointsException(string message)
            : base(message) { }

        public InvalidBasePointsException(string message, Exception innerException)
            : base(message, innerException) { }

        public InvalidBasePointsException(InterpolationCurveType curveType, string additionalMessage)
            : base() 
        {
            this.curveType = curveType;
            this.additionalMessage = additionalMessage;
        }

        public InvalidBasePointsException(string message, InterpolationCurveType curveType, string additionalMessage)
            : base(message)
        {
            this.curveType = curveType;
            this.additionalMessage = additionalMessage;
        }

        public InvalidBasePointsException(string message, InterpolationCurveType curveType, string additionalMessage, Exception innerException)
            : base(message, innerException)
        {
            this.curveType = curveType;
            this.additionalMessage = additionalMessage;
        }
        #endregion

        #region Exception Member
        public override string Message
        {
            get
            {
                string msg = "Invalid base points have been found, so the " + EnumExtension.GetDescriptionFromValue<InterpolationCurveType>(curveType) + " can't be draw as request.";
                if (additionalMessage != "") msg += Environment.NewLine + additionalMessage;
                return msg;
            }
        }
        #endregion
        
        #region ISerializable Member
        private InvalidBasePointsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            additionalMessage = info.GetString("AdditionalMessage");
            curveType = (InterpolationCurveType)info.GetInt32("DrawingType");
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("DrawingType", (int)curveType);
            info.AddValue("AdditionalMessage", additionalMessage);
            base.GetObjectData(info, context);
        }
        #endregion

        #region Property
        public InterpolationCurveType DrawingType
        {
            get
            {
                return curveType;
            }
        }
        #endregion
    }
}
