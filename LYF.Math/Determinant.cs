using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace LYF.Math
{
    /// <summary>
    /// 行列式 Determinant
    /// </summary>
    [SerializableAttribute]
    [ComVisibleAttribute(true)]
    public class Determinant<T> where T : IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
    {
        T[,] tarr = null;
        /// <summary>
        /// 创建n阶行列式
        /// </summary>
        /// <param name="n">阶数</param>
        public Determinant(int n)
        {
            tarr = new T[n, n];
        }

        /// <summary>
        /// 根据二维数组初始化行列式
        /// </summary>
        /// <param name="arrT"></param>
        public Determinant(T[,] arrT)
        {
            if (arrT == null || arrT.GetLength(0) != arrT.GetLength(1) || arrT.GetLength(0) < 1)
            {
                throw new MathException("不正确的数组（数组必须行列数相同且大于1）");
            }
            else
            {
                tarr=new T[arrT.GetLength(0),arrT.GetLength(0)];
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
        /// 获取元素的余子式
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public Determinant<T> A(int i, int j)
        {
            if (N == 1)
            {
                return null;
            }
            else if (i>N||j>N)
            {
                return null;
            }
            else
            {
                Determinant<T> a = new Determinant<T>(N - 1);
                for (int m = 1; m <= N - 1; m++)
                {
                    for (int n = 1; n <= N - 1; n++)
                    {
                        int p = m, q = n;
                        if (p >= i)
                        {
                            p = m + 1;
                        }
                        if (q >= j)
                        {
                            q = n + 1;
                        }
                        a[m, n] = this[p,q];
                    }
                }
                return a;
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
            else if (i > N || j > N)
            {
                throw new MathException("超出行列式索引范围");
            }
            else
            {
                tarr[i - 1, j - 1] = value;
            }
        }

        /// <summary>
        /// 根据二维数组设置行列式的值
        /// </summary>
        /// <param name="arrT"></param>
        public void SetItem(T[,] arrT)
        {
            if (arrT == null || tarr == null)
            {
                throw new MathException("不能为空");
            }
            else if (arrT.GetLength(0) != N || arrT.GetLength(1) != N)
            {
                throw new MathException("传入阶数不同");
            }
            else
            {
                for (int m = 0; m <=N-1; m++)
                {
                    for (int n = 0; n <= N- 1; n++)
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
            else if (i > N || j > N)
            {
                throw new MathException("超出行列式索引范围");
            }
            else
            {
                return tarr[i-1, j-1];
            }
        }

        /// <summary>
        /// 输出行列式信息
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sbRs = new StringBuilder();
            if(tarr!=null)
            {
                for (int m = 0; m <= N - 1; m++)
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
        /// 获取行列式的二维数组
        /// </summary>
        /// <returns></returns>
        public T[,] ToArray()
        {
            return tarr;
        }

        /// <summary>
        /// 获取行列式的阶数
        /// </summary>
        public int N
        {
            get{
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


        private string typeName = string.Empty;
        private string GetType()
        {
            if (string.IsNullOrEmpty(typeName))
            {
                typeName=typeof(T).Name;
                //File.AppendAllText("E:\\op.txt", typeName);
            }
            return typeName;

        }

        /// <summary>
        /// 获取行列式的值
        /// </summary>
        public T Value
        {
            get
            {
                if (N == 1)
                {
                    return tarr[0, 0];
                }
                else if (N == 2)
                {
                    return Minus(MUL(tarr[0, 0], tarr[1, 1]), MUL(tarr[0, 1], tarr[1, 0]));
                }
                else
                {
                    T sum = default(T);
                    for (int i = 1; i <= N; i++)
                    {
                        if ((1+i) % 2 == 0)
                        {
                            //余子式正值
                            sum = Add(sum, MUL(this[1, i], this.A(1, i).Value));
                        }
                        else
                        {
                            //余子式负值
                            sum = Minus(sum, MUL(this[1, i], this.A(1, i).Value));
                        }
                    }
                    return sum;
                }
                
            }
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


    }
}
