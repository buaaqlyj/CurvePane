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

namespace Util.Variable.Matrix
{
    public class LinearEquationSet
    {
        private Matrix coefficientsMatrix = null;
        private Matrix constantsMatrix = null;
        private int degree = 0;
        private bool isValid = false;

        #region Constructor
        public LinearEquationSet() { }

        public LinearEquationSet(Matrix coefficients, Matrix constants)
        {
            this.coefficientsMatrix = coefficients;
            this.constantsMatrix = constants;
            degree = this.constantsMatrix.Rows;
            if (this.coefficientsMatrix.Rows != degree || degree < 1 || this.coefficientsMatrix.IsSingular)
            {
                return;
            }
            isValid = true;
        }
        #endregion

        #region Property
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
                    return Matrix.Zeros(1);
                }
            }
        }
        #endregion
    }
}
