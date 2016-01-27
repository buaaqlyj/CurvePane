using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

using CurvePane.Curve.CurveParam;
using CurvePane.Extension.Tool;

namespace CurvePane.UserExceptions
{
    [Serializable]
    public sealed class SameXInOrderedCurvePointListException : Exception, ISerializable
    {
        private CurveType curveType = CurveType.unknown;

        #region Constructor
        public SameXInOrderedCurvePointListException()
            : base() { }

        public SameXInOrderedCurvePointListException(string message)
            : base(message) { }

        public SameXInOrderedCurvePointListException(string message, Exception innerException)
            : base(message, innerException) { }

        public SameXInOrderedCurvePointListException(CurveType curveType)
            : base() 
        {
            this.curveType = curveType;
        }

        public SameXInOrderedCurvePointListException(string message, CurveType curveType)
            : base(message)
        {
            this.curveType = curveType;
        }

        public SameXInOrderedCurvePointListException(string message, CurveType curveType, Exception innerException)
            : base(message, innerException)
        {
            this.curveType = curveType;
        }
        #endregion

        #region Exception Member
        public override string Message
        {
            get
            {
                string msg = base.Message.Trim();
                if (msg != "") msg += Environment.NewLine;
                msg += "At least two points given have the same X value, so the " + EnumExtension.GetDescriptionFromValue<CurveType>(curveType) + " can't be draw as request.";
                return msg;
            }
        }
        #endregion
        
        #region ISerializable Member
        private SameXInOrderedCurvePointListException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            curveType = (CurveType)info.GetInt32("DrawingType");
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("DrawingType", (int)curveType);
            base.GetObjectData(info, context);
        }
        #endregion

        #region Property
        public CurveType DrawingType
        {
            get
            {
                return curveType;
            }
        }
        #endregion
    }
}
