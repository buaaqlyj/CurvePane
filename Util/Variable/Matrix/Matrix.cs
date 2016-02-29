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
    public class Matrix
    {
        private DoubleExtension[,] _data;

        #region Constructor
        public Matrix(int size)
        {
            this._data = new DoubleExtension[size, size];
        }

        public Matrix(int rows, int cols)
        {
            this._data = new DoubleExtension[rows, cols];
        }

        public Matrix(DoubleExtension[,] data)
        {
            this._data = data;
        }

        public Matrix(Matrix data)
        {
            this._data = data.Data;
        }
        #endregion

        #region Operator
        public static Matrix operator +(Matrix matrix1, Matrix matrix2)
        {
            int matrix1_Rows = matrix1.Data.GetLength(0);
            int matrix1_Columns = matrix1.Data.GetLength(1);

            int matrix2_Rows = matrix2.Data.GetLength(0);
            int matrix2_Columns = matrix2.Data.GetLength(1);

            if ((matrix1_Rows != matrix2_Rows) || (matrix1_Columns != matrix2_Columns))
            {
                throw new Exception("Matrix Dimensions Don't Agree!");
            }

            DoubleExtension[,] result = new DoubleExtension[matrix1_Rows, matrix1_Columns];
            for (int i = 0; i < matrix1_Rows; i++)
            {
                for (int j = 0; j < matrix1_Columns; j++)
                {
                    result[i, j] = matrix1.Data[i, j] + matrix2.Data[i, j];
                }
            }

            return new Matrix(result);
        }

        public static Matrix operator -(Matrix matrix1, Matrix matrix2)
        {
            int matrix1_Rows = matrix1.Data.GetLength(0);
            int matrix1_Columns = matrix1.Data.GetLength(1);

            int matrix2_Rows = matrix2.Data.GetLength(0);
            int matrix2_Columns = matrix2.Data.GetLength(1);

            if ((matrix1_Rows != matrix2_Rows) || (matrix1_Columns != matrix2_Columns))
            {
                throw new Exception("Matrix Dimensions Don't Agree!");
            }

            DoubleExtension[,] result = new DoubleExtension[matrix1_Rows, matrix1_Columns];
            for (int i = 0; i < matrix1_Rows; i++)
            {
                for (int j = 0; j < matrix1_Columns; j++)
                {
                    result[i, j] = matrix1.Data[i, j] - matrix2.Data[i, j];
                }
            }

            return new Matrix(result);
        }

        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
        {
            int matrix1_Rows = matrix1.Data.GetLength(0);
            int matrix1_Columns = matrix1.Data.GetLength(1);

            int matrix2_Rows = matrix2.Data.GetLength(0);
            int matrix2_Columns = matrix2.Data.GetLength(1);

            if (matrix1_Columns != matrix2_Rows)
            {
                throw new Exception("Matrix Dimensions Don't Agree!");
            }

            DoubleExtension[,] result = new DoubleExtension[matrix1_Rows, matrix2_Columns];
            for (int i = 0; i < matrix1_Rows; i++)
            {
                for (int j = 0; j < matrix2_Columns; j++)
                {
                    for (int k = 0; k < matrix2_Rows; k++)
                    {
                        result[i, j] += matrix1.Data[i, k] * matrix2.Data[k, j];
                    }
                }
            }

            return new Matrix(result);
        }

        public static Matrix operator /(DoubleExtension i, Matrix matrix)
        {
            return new Matrix(ScaleBy(i, INV(matrix.Data)));
        }

        public static bool operator ==(Matrix matrix1, Matrix matrix2)
        {
            bool result = true;

            int matrix1_Rows = matrix1.Data.GetLength(0);
            int matrix1_Columns = matrix1.Data.GetLength(1);

            int matrix2_Rows = matrix2.Data.GetLength(0);
            int matrix2_Columns = matrix2.Data.GetLength(1);

            if ((matrix1_Rows != matrix2_Rows) || (matrix1_Columns != matrix2_Columns))
            {
                result = false;
            }
            else
            {
                for (int i = 0; i < matrix1_Rows; i++)
                {
                    for (int j = 0; j < matrix1_Columns; j++)
                    {
                        if (matrix1.Data[i, j] != matrix2.Data[i, j]) result = false;
                    }
                }
            }
            return result;
        }

        public static bool operator !=(Matrix matrix1, Matrix matrix2)
        {
            return !(matrix1 == matrix2);
        }
        #endregion

        #region IEquatable<DoubleExtension> Member
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Matrix)) return false;
            return this == (Matrix)obj;
        }
        #endregion

        #region Matrix.Operation
        public Matrix Inverse()
        {
            if ((this.IsSquare) && (!this.IsSingular))
            {
                return new Matrix(INV(this.Data));
            }
            else
            {
                throw new System.Exception(@"Cannot find inverse for non square /singular matrix");
            }
        }

        public Matrix Transpose()
        {
            DoubleExtension[,] D = Transpose(this.Data);
            return new Matrix(D);
        }
        #endregion

        #region Class.Member
        public static Matrix LinearSolve(Matrix COF, Matrix CON)
        {
            return COF.Inverse() * CON;
        }

        public static Matrix Zeros(int size)
        {
            DoubleExtension[,] D = new DoubleExtension[size, size];
            return new Matrix(D);
        }

        public static Matrix Zeros(int rows, int cols)
        {
            DoubleExtension[,] D = new DoubleExtension[rows, cols];
            return new Matrix(D);
        }
        #endregion

        #region Public.Interface
        public DoubleExtension Det()
        {
            if (this.IsSquare)
            {
                return Determinent(this.Data);
            }
            else
            {
                throw new System.Exception("Cannot Determine the DET for a non square matrix");
            }
        }
        #endregion

        #region Property
        public bool IsSquare
        {
            get
            {
                return (this.Data.GetLength(0) == this.Data.GetLength(1));
            }
        }

        public bool IsSingular
        {
            get
            {
                return (this.Det() == DoubleExtension.Zero);
            }
        }

        public int Rows { get { return this.Data.GetLength(0); } }

        public int Columns { get { return this.Data.GetLength(1); } }
        /// <summary>
        /// Gets or Sets the specified value of the Matrix
        /// </summary>
        /// <param name="i">The row position in the Matrix</param>
        /// <param name="j">The Column position in the Matrix</param>
        /// <returns>Double, The specified value of the Matrix</returns>
        public DoubleExtension this[int i, int j]
        {
            get { return this._data[i, j]; }
            set { this._data[i, j] = value; }
        }
        /// <summary>
        /// Gets the Matrix
        /// </summary>
        public DoubleExtension[,] Data
        {
            get { return _data; }
        }
        #endregion

        #region Private.Methods
        private static DoubleExtension[,] INV(DoubleExtension[,] srcMatrix)
        {
            int rows = srcMatrix.GetLength(0);
            int columns = srcMatrix.GetLength(1);

            if (rows != columns)
                throw new ArgumentException("Cannot find inverse for an non-square matrix", "srcMatrix");

            int q;
            DoubleExtension[,] desMatrix = new DoubleExtension[rows, columns];
            DoubleExtension[,] unitMatrix = UnitMatrix(rows);
            for (int p = 0; p < rows; p++)
            {
                for (q = 0; q < columns; q++)
                {
                    desMatrix[p, q] = srcMatrix[p, q];
                }
            }

            int i = 0;
            DoubleExtension det = DoubleExtension.PositiveOne;
            if (srcMatrix[0, 0] == 0)
            {
                i = 1;
                while (i < rows)
                {
                    if (srcMatrix[i, 0] != 0)
                    {
                        Matrix.InterRow(srcMatrix, 0, i);
                        Matrix.InterRow(unitMatrix, 0, i);
                        det *= -1;
                        break;
                    }

                    i++;
                }
            }

            det *= srcMatrix[0, 0];
            Matrix.RowDiv(unitMatrix, 0, srcMatrix[0, 0]);
            Matrix.RowDiv(srcMatrix, 0, srcMatrix[0, 0]);
            for (int p = 1; p < rows; p++)
            {
                q = 0;
                while (q < p)
                {
                    Matrix.RowSub(unitMatrix, p, q, srcMatrix[p, q]);
                    Matrix.RowSub(srcMatrix, p, q, srcMatrix[p, q]);
                    q++;
                }

                if (srcMatrix[p, p] != 0)
                {
                    det *= srcMatrix[p, p];
                    Matrix.RowDiv(unitMatrix, p, srcMatrix[p, p]);
                    Matrix.RowDiv(srcMatrix, p, srcMatrix[p, p]);
                }

                if (srcMatrix[p, p] == 0)
                {
                    for (int j = p + 1; j < columns; j++)
                    {
                        if (srcMatrix[p, j] != 0)
                        {
                            for (int k = 0; k < rows; k++)
                            {
                                for (q = 0; q < columns; q++)
                                {
                                    srcMatrix[k, q] = desMatrix[k, q];
                                }
                            }

                            return Inverse(desMatrix);
                        }
                    }
                }
            }

            for (int p = rows - 1; p > 0; p--)
            {
                for (q = p - 1; q >= 0; q--)
                {
                    Matrix.RowSub(unitMatrix, q, p, srcMatrix[q, p]);
                    Matrix.RowSub(srcMatrix, q, p, srcMatrix[q, p]);
                }
            }

            for (int p = 0; p < rows; p++)
            {
                for (q = 0; q < columns; q++)
                {
                    srcMatrix[p, q] = desMatrix[p, q];
                }
            }

            return unitMatrix;
        }
        /// <summary>
        /// Inverse the Matrix
        /// </summary>
        /// <param name="srcMatrix">The Matrix to be Inversed</param>
        /// <returns>The Inversed Matrix</returns>
        private static DoubleExtension[,] Inverse(DoubleExtension[,] srcMatrix)
        {
            int rows = srcMatrix.GetLength(0);
            int columns = srcMatrix.GetLength(1);
            DoubleExtension[,] desMatrix = new DoubleExtension[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    desMatrix[i, j] = DoubleExtension.Zero;
                }
            }

            if (rows != columns)
                throw new Exception("Cannot find inverse for an non-square matrix");

            DoubleExtension determine = Determinent(srcMatrix);

            if (determine == DoubleExtension.Zero)
            {
                throw new Exception("Cannot Perform Inversion. Matrix Singular");
            }

            for (int p = 0; p < rows; p++)
            {
                for (int q = 0; q < columns; q++)
                {
                    DoubleExtension[,] tmp = FilterMatrix(srcMatrix, p, q);
                    DoubleExtension determineTMP = Determinent(tmp);
                    desMatrix[p, q] = Math.Pow(-1, p + q + 2) * determineTMP / determine;
                }
            }
            desMatrix = Transpose(desMatrix);
            return desMatrix;
        }
        /// <summary>
        /// Calculate the Determinent
        /// </summary>
        /// <param name="srcMatrix">The Matrix used to calc</param>
        /// <returns>The Determinent</returns>
        private static DoubleExtension Determinent(DoubleExtension[,] srcMatrix)
        {
            int q = 0;
            int rows = srcMatrix.GetLength(0);
            int columns = srcMatrix.GetLength(1);
            DoubleExtension[,] desMatrix = new DoubleExtension[rows, columns];
            for (int p = 0; p < rows; p++)
            {
                for (q = 0; q < columns; q++)
                {
                    desMatrix[p, q] = srcMatrix[p, q];
                }
            }

            int i = 0;
            DoubleExtension det = DoubleExtension.PositiveOne;
            try
            {
                if (rows != columns)
                {
                    throw new Exception("Error: Matrix not Square");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                if (rows == 0)
                {
                    throw new Exception("Dimension of the Matrix 0X0");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (rows == 2)
            {
                return ((srcMatrix[0, 0] * srcMatrix[1, 1]) - (srcMatrix[0, 1] * srcMatrix[1, 0]));
            }

            if (srcMatrix[0, 0] == 0)
            {
                i = 1;
                while (i < rows)
                {
                    if (srcMatrix[i, 0] != 0)
                    {
                        Matrix.InterRow(srcMatrix, 0, i);
                        det *= -1;
                        break;
                    }
                    i++;
                }
            }

            if (srcMatrix[0, 0] == 0) return DoubleExtension.Zero;

            det *= srcMatrix[0, 0];
            Matrix.RowDiv(srcMatrix, 0, srcMatrix[0, 0]);
            for (int p = 1; p < rows; p++)
            {
                q = 0;
                while (q < p)
                {
                    Matrix.RowSub(srcMatrix, p, q, srcMatrix[p, q]);
                    q++;
                }
                if (srcMatrix[p, p] != 0)
                {
                    det *= srcMatrix[p, p];
                    Matrix.RowDiv(srcMatrix, p, srcMatrix[p, p]);
                }
                if (srcMatrix[p, p] == 0)
                {
                    for (int j = p + 1; j < columns; j++)
                    {
                        if (srcMatrix[p, j] != 0)
                        {
                            Matrix.ColumnSub(srcMatrix, p, j, DoubleExtension.NegativeOne);
                            det *= srcMatrix[p, p];
                            Matrix.RowDiv(srcMatrix, p, srcMatrix[p, p]);
                            break;
                        }
                    }
                }

                if (srcMatrix[p, p] == 0) return DoubleExtension.Zero;
            }

            for (int p = 0; p < rows; p++)
            {
                for (q = 0; q < columns; q++)
                {
                    srcMatrix[p, q] = desMatrix[p, q];
                }
            }

            return det;
        }
        /// <summary>
        /// Scale the Matrix by a specified ratio
        /// </summary>
        /// <param name="scalar">The Ratio for the Scale</param>
        /// <param name="srcMatrix">The Matrix which will be scaled</param>
        /// <returns>A new Matrix which is scaled with a specified ratio</returns>
        private static DoubleExtension[,] ScaleBy(DoubleExtension scalar, DoubleExtension[,] srcMatrix)
        {
            int rows = srcMatrix.GetLength(0);
            int columns = srcMatrix.GetLength(1);
            DoubleExtension[,] desMatrix = new DoubleExtension[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    desMatrix[i, j] = scalar * srcMatrix[i, j];
                }
            }

            return desMatrix;
        }
        /// <summary>
        /// To get a unit matrix
        /// </summary>
        /// <param name="dimension">Dimension</param>
        /// <returns>The unit double array</returns>
        private static DoubleExtension[,] UnitMatrix(int dimension)
        {
            DoubleExtension[,] a = new DoubleExtension[dimension, dimension];
            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    if (i == j) a[i, j] = DoubleExtension.PositiveOne;
                    else a[i, j] = DoubleExtension.Zero;
                }
            }

            return a;
        }
        /// <summary>
        /// Scale a specified Row
        /// </summary>
        /// <param name="srcMatrix">The Matrix to be scaled</param>
        /// <param name="row">The Specified Row</param>
        /// <param name="scaleRatio">The Scale Ratio</param>
        private static void RowDiv(DoubleExtension[,] srcMatrix, int row, DoubleExtension scaleRatio)
        {
            int columns = srcMatrix.GetLength(1);
            for (int i = 0; i < columns; i++)
            {
                srcMatrix[row, i] /= scaleRatio;
            }
        }
        /// <summary>
        /// Substract a specified Row from another Row
        /// </summary>
        /// <param name="srcMatrix">The Matrix to be substract</param>
        /// <param name="row1">The Row Index to be substracted</param>
        /// <param name="row2">The Row Index to substract</param>
        /// <param name="scaleRatio">Scale Ratio</param>
        private static void RowSub(DoubleExtension[,] srcMatrix, int row1, int row2, DoubleExtension scaleRatio)
        {
            int columns = srcMatrix.GetLength(1);
            for (int q = 0; q < columns; q++)
            {
                srcMatrix[row1, q] -= scaleRatio * srcMatrix[row2, q];
            }
        }
        /// <summary>
        /// Substract a specified Column from another Column
        /// </summary>
        /// <param name="srcMatrix">The Matrix to be substracted</param>
        /// <param name="column1">The Column Index to be Substracted</param>
        /// <param name="column2">The Column Index to Substract</param>
        /// <param name="scaleRatio">Scale Ratio</param>
        private static void ColumnSub(DoubleExtension[,] srcMatrix, int column1, int column2, DoubleExtension scaleRatio)
        {
            int rows = srcMatrix.GetLength(0);
            int columns = srcMatrix.GetLength(1);
            for (int i = 0; i < rows; i++)
            {
                srcMatrix[i, column1] -= scaleRatio * srcMatrix[i, column2];
            }
        }
        /// <summary>
        /// Exchange a specified row's value
        /// </summary>
        /// <param name="srcMatrix">The Matrix to be exchanged</param>
        /// <param name="row1">Row index</param>
        /// <param name="row2">Row index</param>
        /// <returns>Exchanged Matrix</returns>
        private static DoubleExtension[,] InterRow(DoubleExtension[,] srcMatrix, int row1, int row2)
        {
            int rows = srcMatrix.GetLength(0);
            int columns = srcMatrix.GetLength(1);
            DoubleExtension tmp = DoubleExtension.Zero;
            for (int k = 0; k < columns; k++)
            {
                tmp = srcMatrix[row1, k];
                srcMatrix[row1, k] = srcMatrix[row2, k];
                srcMatrix[row2, k] = tmp;
            }

            return srcMatrix;
        }
        /// <summary>
        /// To Filter the Matrix
        /// </summary>
        /// <param name="srcMatrix">The Matrix to be Filtered</param>
        /// <param name="row">A specified Row</param>
        /// <param name="column">A specified Column</param>
        /// <returns>The Filtered Matrix</returns>
        private static DoubleExtension[,] FilterMatrix(DoubleExtension[,] srcMatrix, int row, int column)
        {
            int rows = srcMatrix.GetLength(0);
            DoubleExtension[,] desMatrix = new DoubleExtension[rows - 1, rows - 1];
            int i = 0;
            for (int p = 0; p < rows; p++)
            {
                int j = 0;
                if (p != row)
                {
                    for (int q = 0; q < rows; q++)
                    {
                        if (q != column)
                        {
                            desMatrix[i, j] = srcMatrix[p, q];
                            j++;
                        }
                    }

                    i++;
                }
            }

            return desMatrix;
        }
        /// <summary>
        /// Transpose Matrix
        /// </summary>
        /// <param name="srcMatrix">The Matrix to be Transposed</param>
        /// <returns>The Transposed Matrix</returns>
        private static DoubleExtension[,] Transpose(DoubleExtension[,] srcMatrix)
        {
            int rows = srcMatrix.GetLength(0);
            int columns = srcMatrix.GetLength(1);
            DoubleExtension[,] desMatrix = new DoubleExtension[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    desMatrix[j, i] = srcMatrix[i, j];
                }
            }

            return desMatrix;
        }
        #endregion
    }
}
