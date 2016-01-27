using System;
using System.Collections.Generic;
using System.Text;

using CurvePane.Extension.Variable;

namespace CurvePane.Curve.CurveElements.Point
{
    public class CurvePoint
    {
        private DoubleExtension x;
        private DoubleExtension y;

        #region Constructor
        public CurvePoint(double x, double y)
        {
            this.x = new DoubleExtension(x);
            this.y = new DoubleExtension(y);
        }
        public CurvePoint(int x, int y)
        {
            this.x = new DoubleExtension(x);
            this.y = new DoubleExtension(y);
        }
        #endregion

        #region Property
        public DoubleExtension X
        {
            get
            {
                return x;
            }
        }
        public DoubleExtension Y
        {
            get
            {
                return y;
            }
        }
        #endregion

        #region Public.Interface
        public double distance(CurvePoint pt)
        {
            return Math.Sqrt(Math.Pow((pt.X - this.X).CoordinateValue, 2) + Math.Pow((pt.Y - this.Y).CoordinateValue, 2));
        }
        #endregion

        #region Class.Interface
        public static bool hasSameX(CurvePoint pt1, CurvePoint pt2)
        {
            return pt1.X == pt2.X;
        }

        public static double distance(CurvePoint pt1, CurvePoint pt2)
        {
            return Math.Sqrt(Math.Pow((pt1.X - pt2.X).CoordinateValue, 2) + Math.Pow((pt1.Y - pt2.Y).CoordinateValue, 2));
        }
        #endregion
    }
}
