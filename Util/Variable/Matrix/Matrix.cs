using System;
using System.Collections.Generic;
using System.Text;

namespace Util.Variable.Matrix
{
    public class Matrix
    {
        private double[,] _data;

        #region Constructor
        public Matrix(int size)
        {
            this._data = new double[size, size];
        }

        public Matrix(int rows, int cols)
        {
            this._data = new double[rows, cols];
        }

        public Matrix(double[,] data)
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

            double[,] result = new double[matrix1_Rows, matrix1_Columns];
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

            double[,] result = new double[matrix1_Rows, matrix1_Columns];
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

            double[,] result = new double[matrix1_Rows, matrix2_Columns];
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

        public static Matrix operator /(double i, Matrix matrix)
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
            double[,] D = Transpose(this.Data);
            return new Matrix(D);
        }
        #endregion

        #region Class.Member
        public static Matrix Zeros(int size)
        {
            double[,] D = new double[size, size];
            return new Matrix(D);
        }

        public static Matrix Zeros(int rows, int cols)
        {
            double[,] D = new double[rows, cols];
            return new Matrix(D);
        }
        #endregion

        #region Public.Interface
        public Matrix LinearSolve(Matrix COF, Matrix CON)
        {
            return COF.Inverse() * CON;
        }

        public double Det()
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
                return ((int)this.Det() == 0);
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
        public double this[int i, int j]
        {
            get { return this._data[i, j]; }
            set { this._data[i, j] = value; }
        }
        /// <summary>
        /// Gets the Matrix
        /// </summary>
        public double[,] Data
        {
            get { return _data; }
        }

        public double[] DataArray
        {
            get
            {
                double[] result = new double[Rows * Columns];
                int k = 0;
                for (int i = 0; i < Rows; i++)
                    for (int j = 0; j < Columns; j++)
                        result[k++] = this[i, j];
                return result;
            }
        }
        #endregion

        #region Private.Methods
        private static double[,] INV(double[,] srcMatrix)
        {
            int rows = srcMatrix.GetLength(0);
            int columns = srcMatrix.GetLength(1);
            try
            {
                if (rows != columns)
                    throw new Exception();
            }
            catch
            {
                Console.WriteLine("Cannot find inverse for an non-square matrix");
            }

            int q;
            double[,] desMatrix = new double[rows, columns];
            double[,] unitMatrix = UnitMatrix(rows);
            for (int p = 0; p < rows; p++)
            {
                for (q = 0; q < columns; q++)
                {
                    desMatrix[p, q] = srcMatrix[p, q];
                }
            }

            int i = 0;
            double det = 1;
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
        private static double[,] Inverse(double[,] srcMatrix)
        {
            int rows = srcMatrix.GetLength(0);
            int columns = srcMatrix.GetLength(1);
            double[,] desMatrix = new double[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    desMatrix[i, j] = 0;
                }
            }

            try
            {
                if (rows != columns) throw new Exception();
            }
            catch
            {
                Console.WriteLine("Cannot Find Inverse for an non-square Matrix");
            }

            double determine = Determinent(srcMatrix);

            try
            {
                if (determine == 0)
                {
                    throw new Exception("Cannot Perform Inversion. Matrix Singular");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            for (int p = 0; p < rows; p++)
            {
                for (int q = 0; q < columns; q++)
                {
                    double[,] tmp = FilterMatrix(srcMatrix, p, q);
                    double determineTMP = Determinent(tmp);
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
        private static double Determinent(double[,] srcMatrix)
        {
            int q = 0;
            int rows = srcMatrix.GetLength(0);
            int columns = srcMatrix.GetLength(1);
            double[,] desMatrix = new double[rows, columns];
            for (int p = 0; p < rows; p++)
            {
                for (q = 0; q < columns; q++)
                {
                    desMatrix[p, q] = srcMatrix[p, q];
                }
            }

            int i = 0;
            double det = 1;
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

            if (srcMatrix[0, 0] == 0) return 0;

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
                            Matrix.ColumnSub(srcMatrix, p, j, -1);
                            det *= srcMatrix[p, p];
                            Matrix.RowDiv(srcMatrix, p, srcMatrix[p, p]);
                            break;
                        }
                    }
                }

                if (srcMatrix[p, p] == 0) return 0;
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
        private static double[,] ScaleBy(double scalar, double[,] srcMatrix)
        {
            int rows = srcMatrix.GetLength(0);
            int columns = srcMatrix.GetLength(1);
            double[,] desMatrix = new double[rows, columns];
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
        private static double[,] UnitMatrix(int dimension)
        {
            double[,] a = new double[dimension, dimension];
            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    if (i == j) a[i, j] = 1;
                    else a[i, j] = 0;
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
        private static void RowDiv(double[,] srcMatrix, int row, double scaleRatio)
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
        private static void RowSub(double[,] srcMatrix, int row1, int row2, double scaleRatio)
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
        private static void ColumnSub(double[,] srcMatrix, int column1, int column2, double scaleRatio)
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
        private static double[,] InterRow(double[,] srcMatrix, int row1, int row2)
        {
            int rows = srcMatrix.GetLength(0);
            int columns = srcMatrix.GetLength(1);
            double tmp = 0;
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
        private static double[,] FilterMatrix(double[,] srcMatrix, int row, int column)
        {
            int rows = srcMatrix.GetLength(0);
            double[,] desMatrix = new double[rows - 1, rows - 1];
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
        private static double[,] Transpose(double[,] srcMatrix)
        {
            int rows = srcMatrix.GetLength(0);
            int columns = srcMatrix.GetLength(1);
            double[,] desMatrix = new double[rows, columns];
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
