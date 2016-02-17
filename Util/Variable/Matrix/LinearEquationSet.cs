using System;
using System.Collections.Generic;
using System.Text;

namespace Util.Variable.Matrix
{
    /// <summary>
    /// 求解线性方程组的类LEquations
    /// 用到的Matrix成员和方法：
    /// 1. double[] GetData()
    ///   返回矩阵按顺序排列的一维数组。
    /// 2. int GetNumColumns()
    ///   返回矩阵列数
    /// 3. int GetNumRows()
    ///   返回矩阵行数
    /// 4. void SetValue(Matrix m)
    ///   把m矩阵的内容赋值给this
    /// 5. 构造方法 Matrix(Matrix m)
    ///   用m矩阵的内容初始化自己
    /// </summary>
    public class LinearEquationSet
    {
        #region 线性方程组类的构造函数及一般方法
        private Matrix mtxLECoef;  // 系数矩阵
        private Matrix mtxLEConst;  // 常数矩阵
        /// <summary>
        /// 基本构造函数
        /// </summary>
        public LinearEquationSet()
        {
        }
     
        /// <summary>
        /// 指定系数和常数的构造函数
       /// </summary>
        /// <param name="mtxCoef">指定的系数矩阵</param>
        /// <param name="mtxConst">指定的常数矩阵</param>
        public LinearEquationSet(Matrix mtxCoef, Matrix mtxConst)
        {
            Init(mtxCoef, mtxConst);
        }
     
        /// <summary>
        /// 初始化函数
       /// </summary>
        /// <param name="mtxCoef">指定的系数矩阵</param>
        /// <param name="mtxConst">指定的常数矩阵</param>
        public bool Init(Matrix mtxCoef, Matrix mtxConst)
        {
            if (mtxCoef.Rows != mtxConst.Rows)
                return false;
            mtxLECoef = new Matrix(mtxCoef);
            mtxLEConst = new Matrix(mtxConst);
            return true;
        }
        /// <summary>
        /// 获取系数矩阵
        /// </summary>
        /// <returns> Matrix 型，返回系数矩阵</returns>
        public Matrix GetCoefMatrix()
        {
            return mtxLECoef;
        }
      
        /// <summary>
        /// 获取常数矩阵
        /// </summary>
        public Matrix GetConstMatrix()
        {
            return mtxLEConst;
        }
        /// <summary>
        /// 获取方程个数
        /// </summary>
        public int GetNumEquations()
        {
            return GetCoefMatrix().Rows;
        }
        /// <summary>
        /// 获取未知数个数
        /// </summary>
        public int GetNumUnknowns()
        {
            return GetCoefMatrix().Columns;
        }
        #endregion 
        /// <summary>
        /// 全选主元高斯消去法
        /// </summary>
        /// <param name="mtxResult"></param>
        /// <returns></returns>
        public bool GetRootsetGauss(out Matrix mtxResult)
        {
            int l, k, i, j, nIs = 0, p, q;
            double d, t;
            // 方程组的属性，将常数矩阵赋给解矩阵
            mtxResult = new Matrix(mtxLEConst);
            double[] pDataCoef = mtxLECoef.DataArray;
            double[] pDataConst = mtxResult.DataArray;
            int n = GetNumUnknowns();
            // 临时缓冲区，存放列数
            int[] pnJs = new int[n];
            // 消元
            l = 1;
            for (k = 0; k <= n - 2; k++)
            {
                d = 0.0;
                for (i = k; i <= n - 1; i++)
                {
                    for (j = k; j <= n - 1; j++)
                    {
                        t = Math.Abs(pDataCoef[i * n + j]);
                        if (t > d)
                        {
                            d = t;
                            pnJs[k] = j;
                            nIs = i;
                        }
                    }
                }
                if (d == 0.0)
                    l = 0;
                else
                {
                    if (pnJs[k] != k)
                    {
                        for (i = 0; i <= n - 1; i++)
                        {
                            p = i * n + k;
                            q = i * n + pnJs[k];
                            t = pDataCoef[p];
                            pDataCoef[p] = pDataCoef[q];
                            pDataCoef[q] = t;
                        }
                    }
                    if (nIs != k)
                    {
                        for (j = k; j <= n - 1; j++)
                        {
                            p = k * n + j;
                            q = nIs * n + j;
                            t = pDataCoef[p];
                            pDataCoef[p] = pDataCoef[q];
                            pDataCoef[q] = t;
                        }

                        t = pDataConst[k];
                        pDataConst[k] = pDataConst[nIs];
                        pDataConst[nIs] = t;
                    }
                }

                // 求解失败
                if (l == 0)
                {
                    return false;
                }

                d = pDataCoef[k * n + k];
                for (j = k + 1; j <= n - 1; j++)
                {
                    p = k * n + j;
                    pDataCoef[p] = pDataCoef[p] / d;
                }

                pDataConst[k] = pDataConst[k] / d;
                for (i = k + 1; i <= n - 1; i++)
                {
                    for (j = k + 1; j <= n - 1; j++)
                    {
                        p = i * n + j;
                        pDataCoef[p] = pDataCoef[p] - pDataCoef[i * n + k] * pDataCoef[k * n + j];
                    }

                    pDataConst[i] = pDataConst[i] - pDataCoef[i * n + k] * pDataConst[k];
                }
            }

            // 求解失败
            d = pDataCoef[(n - 1) * n + n - 1];
            if (d == 0.0)
            {
                return false;
            }
            // 求解
            pDataConst[n - 1] = pDataConst[n - 1] / d;
            for (i = n - 2; i >= 0; i--)
            {
                t = 0.0;
                for (j = i + 1; j <= n - 1; j++)
                    t = t + pDataCoef[i * n + j] * pDataConst[j];
                pDataConst[i] = pDataConst[i] - t;
            }

            // 调整解的位置
            pnJs[n - 1] = n - 1;
            for (k = n - 1; k >= 0; k--)
            {
                if (pnJs[k] != k)
                {
                    t = pDataConst[k];
                    pDataConst[k] = pDataConst[pnJs[k]];
                    pDataConst[pnJs[k]] = t;
                }
            }
            return true;
        }
    }
}
