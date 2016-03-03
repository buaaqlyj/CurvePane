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

namespace Util.Variable.Matrix
{
    public class LinearEquationSet
    {
        private Matrix coefficientsMatrix = null;
        private Matrix constantsMatrix = null;
        private int degree = 0;
        private bool isValid = false;

        #region Constructor
        public LinearEquationSet(Matrix coefficients, Matrix constants)
        {
            this.coefficientsMatrix = coefficients;
            this.constantsMatrix = constants;
            degree = this.constantsMatrix.Rows;
            if (this.coefficientsMatrix.Rows != degree)
            {
                throw new ArgumentException("The row count of coefficient matrix is different from that of constant matrix.");
            }
            if (degree < 1)
            {
                throw new ArgumentException("The row counts of both matrices are 0.");
            }
            if (this.coefficientsMatrix.IsSingular)
            {
                throw new ArgumentException("The coefficient matrix is singular.");
            }
            isValid = true;
        }
        #endregion

        #region Property
        public bool IsValid
        {
            get
            {
                return isValid;
            }
        }

        public int Degree
        {
            get
            {
                return degree;
            }
        }

        public Matrix Coefficients
        {
            get
            {
                return coefficientsMatrix;
            }
        }

        public Matrix Constants
        {
            get
            {
                return constantsMatrix;
            }
        }

        public Matrix AnswerMatrix
        {
            get
            {
                if (isValid)
                {
                    return Matrix.LinearSolve(coefficientsMatrix, constantsMatrix);
                }
                else
                {
                    throw new ArgumentException("The equation set can't be solved.");
                }
            }
        }
        #endregion
    }
}
