using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace LYF.Math
{
    /// <summary>
    /// 矩阵
    /// </summary>
    [SerializableAttribute]
    [ComVisibleAttribute(true)]
    public class Matrix<T> where T : IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
    {
        T[,] tarr = null;
        /// <summary>
        /// 创建m行n列的矩阵
        /// </summary>
        /// <param name="m">行数</param>
        /// <param name="n">列数</param>
        public Matrix(int m,int n)
        {
            tarr = new T[m, n];
        }

        /// <summary>
        /// 根据二维数组创建矩阵，矩阵的行数列数分别对应该二维数组的行数列数。
        /// </summary>
        /// <param name="arrT">二维数组</param>
        public Matrix(T[,] arrT)
        {
            if (arrT == null || arrT.GetLength(0)<=0||arrT.GetLength(1)<=0)
            {
                throw new MathException("不正确的数组（数组必须行列数相同且大于1）");
            }
            else
            {
                tarr = new T[arrT.GetLength(0), arrT.GetLength(1)];
                SetItem(arrT);
            }
        }

        /// <summary>
        /// 获取元素值
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public T this[int i, int j]
        {
            //实现索引器的get方法
            get
            {
                return GetItem(i, j);
            }

            //实现索引器的set方法
            set
            {
                SetItem(i, j, value);
            }
        }

        /// <summary>
        /// 设置行列式的值
        /// </summary>
        /// <param name="i">行数（从1开始）</param>
        /// <param name="j">列数（从1开始）</param>
        /// <param name="value">值</param>
        public void SetItem(int i, int j, T value)
        {
            if (tarr == null)
            {
                throw new MathException("行列式未正确初始化");
            }
            else if (i > M || j > N)
            {
                throw new MathException("超出行列式索引范围");
            }
            else
            {
                tarr[i - 1, j - 1] = value;
            }
        }

        /// <summary>
        /// 根据二维数组设置矩阵
        /// </summary>
        /// <param name="arrT">二维数组</param>
        public void SetItem(T[,] arrT)
        {
            if (arrT == null || tarr == null)
            {
                throw new MathException("不能为空");
            }
            else if (arrT.GetLength(0) != M || arrT.GetLength(1) != N)
            {
                throw new MathException("传入行列数与当前矩阵的行列书不匹配");
            }
            else
            {
                for (int m = 0; m <= M - 1; m++)
                {
                    for (int n = 0; n <= N - 1; n++)
                    {
                        this[m + 1, n + 1] = arrT[m, n];
                    }
                }
            }
        }

        /// <summary>
        /// 设置行列式的值
        /// </summary>
        /// <param name="i">行数（从1开始）</param>
        /// <param name="j">列数（从1开始）</param>
        /// <param name="value">值</param>
        public T GetItem(int i, int j)
        {
            if (tarr == null)
            {
                throw new MathException("行列式未正确初始化");
            }
            else if (i > M || j > N)
            {
                throw new MathException("超出行列式索引范围");
            }
            else
            {
                return tarr[i - 1, j - 1];
            }
        }

        /// <summary>
        /// 输出行列式信息
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sbRs = new StringBuilder();
            if (tarr != null)
            {
                for (int m = 0; m <= M - 1; m++)
                {
                    for (int n = 0; n <= N - 1; n++)
                    {
                        sbRs.Append(string.Format("{0}\t", tarr[m, n]));
                    }
                    sbRs.Append("\n");
                }

            }
            return sbRs.ToString();
        }

        /// <summary>
        /// 获取矩阵的行数
        /// </summary>
        public int M
        {
            get
            {
                if (tarr != null)
                {
                    return tarr.GetLength(0);
                }
                else
                {
                    return 0;
                }
            }

        }

        /// <summary>
        /// 获取矩阵的列数
        /// </summary>
        public int N
        {
            get
            {
                if (tarr != null)
                {
                    return tarr.GetLength(1);
                }
                else
                {
                    return 0;
                }
            }

        }

        /// <summary>
        /// 使用高斯消元法 获得梯形矩阵
        /// </summary>
        /// <returns>梯形矩阵</returns>
        public Matrix<T> GaussElimination()
        {
            Matrix<T> newMatrix = this.Copy();

            //消第i行后边的i+1列
            for (int j = 2; j <= M; j++)
            {
                //列值 后行*前行第一项- 前行*厚行第一项
                for (int k = j; k <= M; k++)
                {
                    T T0 = newMatrix[j - 1, j - 1];
                    T T1 = newMatrix[k, j - 1];
                    for (int m = j - 1; m <= N; m++)
                    {
                        T t = newMatrix[k, m];
                        T t0 = newMatrix[j-1, m];
                        newMatrix[k, m] = Minus(MUL(t,T0), MUL(t0, T1));
                    }
                }
                //Console.WriteLine("置换后:"+"");
                //Console.WriteLine(newMatrix.ToString());
            }
            return newMatrix;
        }

        /// <summary>
        /// 获取转置矩阵
        /// </summary>
        /// <returns></returns>
        public Matrix<T> Transposed()
        {
            Matrix<T> newMatrix = new Matrix<T>(this.N, this.M);

            for (int m = 1; m <= M; m++)
            {
                for (int n = 1; n <= N; n++)
                {
                    newMatrix[n,m] = this[m, n];
                }
            }

            return newMatrix;
        }

        /// <summary>
        /// 拷贝矩阵
        /// </summary>
        /// <returns></returns>
        public Matrix<T> Copy()
        {
            Matrix<T> newMatrix = new Matrix<T>(this.M, this.N);

            for (int m = 1; m <= M; m++)
            {
                for (int n = 1; n <= N; n++)
                {
                    newMatrix[m, n] = this[m, n];
                }
            }

            return newMatrix;
        }
  
        /// <summary>
        /// 矩阵加法
        /// </summary>
        /// <param name="left">左</param>
        /// <param name="right">右</param>
        /// <returns>两矩阵加法和</returns>
        public static Matrix<T> operator +(Matrix<T> left, Matrix<T> right)
        {
            if (left == null || right == null || left.M <= 0 || right.N <= 0 || left.M != right.M || left.N != right.N)
            {
                throw new MathException("不符合加法运算条件 要求行列不为0 并且两个矩阵的行数列数同时相同");
            }
            Matrix<T> newMatrix = new Matrix<T>(left.M, left.N);

            for (int m = 1; m <= left.M; m++)
            {
                for (int n = 1; n <= left.N; n++)
                {
                    newMatrix[m, n] = newMatrix.Add(left[m, n], right[m, n]);
                }
            }

            return newMatrix;
        }

        /// <summary>
        /// 矩阵数量乘法
        /// </summary>
        /// <param name="left">左</param>
        /// <param name="right">右</param>
        /// <returns>两矩阵加法和</returns>
        public static Matrix<T> operator *(T left, Matrix<T> right)
        {
            if (left == null || right == null ||  right.N <= 0 )
            {
                throw new MathException("不符合加法运算条件 要求行列不为0");
            }
            Matrix<T> newMatrix = new Matrix<T>(right.M, right.N);

            for (int m = 1; m <= right.M; m++)
            {
                for (int n = 1; n <= right.N; n++)
                {
                    newMatrix[m, n] = newMatrix.MUL(left, right[m, n]);
                }
            }

            return newMatrix;
        }

        /// <summary>
        /// 矩阵乘法
        /// </summary>
        /// <param name="left">左</param>
        /// <param name="right">右</param>
        /// <returns>两矩阵加法和</returns>
        public static Matrix<T> operator *(Matrix<T> left, Matrix<T> right)
        {
            if (left == null || right == null || left.M <= 0 || right.N <= 0||left.N!=right.M)
            {
                throw new MathException("不符合矩阵乘法运算条件 要求行列不为0 并且第一个行数的行数等于第二个行数的列数");
            }
            Matrix<T> newMatrix = new Matrix<T>(left.M, right.N);

            for (int m = 1; m <= newMatrix.M; m++)
            {
                for (int n = 1; n <= newMatrix.N; n++)
                {
                    // left 第一行 * right 第一列
                    for (int i = 1; i <= left.N; i++)
                    {
                        newMatrix[m, n] = newMatrix.Add(left[m, i], right[i, n]);
                    }
                        
                }
            }

            return newMatrix;
        }

        /// <summary>
        /// 比较矩阵是否不相等
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Matrix<T> left, Matrix<T> right)
        {
            return false;
        }

        /// <summary>
        /// 比较矩阵是否相等
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Matrix<T> left, Matrix<T> right)
        {
            return false;
        }

        /// <summary>
        /// 判断该矩阵是否与目标矩阵相同
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public bool Equals(Matrix<T> matrix)
        {
            return false;
        }

        /// <summary>
        /// 获取矩阵的二维数组
        /// </summary>
        /// <returns></returns>
        public T[,] ToArray()
        {
            return tarr;
        }

        #region 私有方法
        private string typeName = string.Empty;
        /// <summary>
        /// 获取T类型
        /// </summary>
        /// <returns></returns>
        private string GetType()
        {
            if (string.IsNullOrEmpty(typeName))
            {
                typeName = typeof(T).Name;
                //File.AppendAllText("E:\\op.txt", typeName);
            }
            return typeName;

        }

        /// <summary>
        /// 加法
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        private T Add(T left, T right)
        {
            switch (GetType())
            {
                case "Int16":
                    return ((T)(object)((short)(object)left + (short)(object)right));
                case "Int32":
                    return ((T)(object)((int)(object)left + (int)(object)right));
                case "Int64":
                    return ((T)(object)((long)(object)left + (long)(object)right));
                case "Single":
                    return ((T)(object)((float)(object)left + (float)(object)right));
                case "Double":
                    return ((T)(object)((double)(object)left + (double)(object)right));
                case "Decimal":
                    return ((T)(object)((decimal)(object)left + (decimal)(object)right));
            }
            throw new MathException("不支持的操作类型");
        }

        /// <summary>
        /// 减法
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        private T Minus(T left, T right)
        {
            switch (GetType())
            {
                case "Int16":
                    return ((T)(object)((short)(object)left - (short)(object)right));
                case "Int32":
                    return ((T)(object)((int)(object)left - (int)(object)right));
                case "Int64":
                    return ((T)(object)((long)(object)left - (long)(object)right));
                case "Single":
                    return ((T)(object)((float)(object)left - (float)(object)right));
                case "Double":
                    return ((T)(object)((double)(object)left - (double)(object)right));
                case "Decimal":
                    return ((T)(object)((decimal)(object)left - (decimal)(object)right));
            }
            throw new MathException("不支持的操作类型");
        }

        /// <summary>
        /// 乘法
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        private T MUL(T left, T right)
        {
            switch (GetType())
            {
                case "Int16":
                    return ((T)(object)((short)(object)left * (short)(object)right));
                case "Int32":
                    return ((T)(object)((int)(object)left * (int)(object)right));
                case "Int64":
                    return ((T)(object)((long)(object)left * (long)(object)right));
                case "Single":
                    return ((T)(object)((float)(object)left * (float)(object)right));
                case "Double":
                    return ((T)(object)((double)(object)left * (double)(object)right));
                case "Decimal":
                    return ((T)(object)((decimal)(object)left * (decimal)(object)right));
            }
            throw new MathException("不支持的操作类型");
        }
        #endregion
    }
}
