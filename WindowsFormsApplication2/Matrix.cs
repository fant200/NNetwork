using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    public class Matrix
    {
        double[,] elements;
        public int Length => elements.GetLength(0);
        public int Width => elements.GetLength(1);
        public double this[int width,int length]
        {
            get { return elements[width,length]; }
            set { elements[width,length] = value; }
        }

        Matrix()
        { }
        Matrix(int width,int length)
        {
            elements = new double[width, length];
        }
        Matrix(double[,] list)
        {
            elements = list;
        }

        static void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp = lhs;
            lhs = rhs;
            rhs = temp;
        }


        public static Matrix operator* (Matrix m1, Matrix m2)
        {
            Matrix temp = new Matrix(Math.Max(m1.Width,m2.Width), Math.Max(m1.Length,m2.Length));
            
            if ((m1.Width == 1 || m2.Width == 1) && (m1.Length == 1 || m2.Length ==1))
            {
                Matrix longer = m2;
                Matrix wider = m2;
                if (m1.Length > m2.Length)
                    longer = m1;
                if (m1.Width > m2.Width)
                    wider = m1;

                for (int i = 0; i < temp.Width; i++)
                {
                    for (int j = 0; j < temp.Length; j++)
                    {
                        temp[i, j] = wider[i, 0] * longer[0, j];
                    }
                }
                return temp;
            }
            else if (m1.Length == m2.Length && m1.Width == m2.Width)
            {
                for (int i = 0; i < temp.Width; i++)
                {
                    for (int j = 0; j < temp.Length; j++)
                    {
                        temp[i, j] = m1[i, j] * m2[i, j];
                    }
                }
                return temp;
            }
            else throw new NotImplementedException();    
        }

        public static Matrix operator+(Matrix m1, Matrix m2)
        {
            Matrix temp = new Matrix(m1.Length, m1.Width);
            if(m1.Width == m2.Width && m1.Length == m2.Length)
            {
                for (int i = 0; i < m1.Width; i++)
                {
                    for (int j = 0; j < m1.Length; j++)
                    {
                        temp[i, j] = m1[i, j] + m2[i, j];
                    }
                }
                return temp;
            }
            throw new NotSupportedException();
        }

        public Matrix SumByLength(Matrix m1)
        {
            Matrix tmp = new Matrix(m1.Width, 1);
            for (int i = 0; i < m1.Length; i++)
            {
                for (int j = 0; j < m1.Width; j++)
                {
                    tmp[j, 0] += m1[i, j];
                }
            }
            return tmp;
        }

        public Matrix SumByWidth(Matrix m1)
        {
            Matrix tmp = new Matrix(1, m1.Length);
            for (int i = 0; i < m1.Width; i++)
            {
                for (int j = 0; j < m1.Length; j++)
                {
                    tmp[0, j] += m1[j, i];
                }
            }
            return tmp;
        }

    }
}
