using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LYF.Math.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 行列式
            ////int[,] aaa =new int[4,4]{{1,2,34,4},{5,6,67,8},{1,2,3,4},{5,6,7,8}};
            ////int[,] aaa = new int[3, 3] { { 1, 2,3 }, { 4,5,7},{ 7, 8,9 } };
            //int[,] aaa = new int[4, 4]{{1,2,3,6},
            //                            {4,5,7,8},
            //                            {7,8,9,10},
            //                            {3,8,4,3}};

            ////LYF.Math.Determinant<int> d = new Determinant<int>(4);
            //LYF.Math.Determinant<int> d = new Determinant<int>(aaa);
            //d.SetItem(aaa);
            //Console.WriteLine("当前行列式：");
            //Console.WriteLine(d.ToString());
            //Console.WriteLine("余子式M11：");
            //Console.WriteLine(d.A(1, 1).ToString());
            //Console.WriteLine("余子式M12：");
            //Console.WriteLine(d.A(1, 2).ToString());
            //Console.WriteLine("余子式M22：");
            //Console.WriteLine(d.A(2, 2).ToString());
            //Console.WriteLine("N="+d.N);
            //Console.WriteLine("行列式的值为:"+d.Value.ToString());
            //Console.Read();
            #endregion

            #region 矩阵
            //int[,] aaa =new int[4,4]{{1,2,34,4},{5,6,67,8},{1,2,3,4},{5,6,7,8}};
            //int[,] aaa = new int[3, 3] { { 1, 2,3 }, { 4,5,7},{ 7, 8,9 } };
            int[,] aaa = new int[5, 4]{{1,2,3,6},
                                        {4,5,7,8},
                                        {7,8,9,10},
                                        {3,8,4,3} ,
                                        {0,0,0,0}};

            int[,] bbb = new int[5, 4]{{1,2,3,6},
                                        {4,5,7,8},
                                        {7,8,9,10},
                                        {3,8,4,3} ,
                                        {0,0,0,0}};

            int[,] ccc = new int[4, 5]{{1,2,3,6,3},
                                        {4,5,7,8,4},
                                        {7,8,9,10,2},
                                        {3,8,4,3,2}};
            //int[,] aaa = new int[2, 2]{{1,2},{3,4}};


            Matrix<int> d = new Matrix<int>(aaa);
            Matrix<int> a = new Matrix<int>(aaa);
            Matrix<int> b = new Matrix<int>(bbb);
            Matrix<int> c = new Matrix<int>(ccc);
            d.SetItem(aaa);
            Console.WriteLine("当前矩阵：");
            Console.WriteLine(d.ToString());
            Console.WriteLine("当前矩阵的转置矩阵：");
            Console.WriteLine(d.Transposed().ToString());
            Console.WriteLine("高斯消元后的梯形矩阵：");
            Console.WriteLine(d.GaussElimination().ToString());
            Console.WriteLine("aaa+bbb：");
            Console.WriteLine(a+b);
            Console.WriteLine("10*b：");
            Console.WriteLine(10* b);
            Console.WriteLine("a*c：");
            Console.WriteLine(a * c);
            //Console.WriteLine(a + c); //直接异常
            Console.Read();
            #endregion
        }
    }
}
