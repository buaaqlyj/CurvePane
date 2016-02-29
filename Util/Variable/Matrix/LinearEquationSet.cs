using System;
using System.Collections.Generic;
using System.Text;

namespace Util.Variable.Matrix
{
    /// <summary>
    /// 
    /// </summary>
    public class LinearEquationSet
    {
        private Matrix coefficientsMatrix = null;
        private Matrix constantsMatrix = null;

        #region Constructor
        public LinearEquationSet() { }

        public LinearEquationSet(Matrix coefficients, Matrix constants)
        {
            
            this.coefficientsMatrix = coefficients;
            this.constantsMatrix = constants;
        }
        #endregion

        #region Property
        public Matrix Solve
        {
            get
            {
                return Matrix.LinearSolve(coefficientsMatrix, constantsMatrix);
            }
        }
        #endregion
    }
}
