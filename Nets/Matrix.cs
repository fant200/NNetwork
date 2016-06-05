using System;

namespace Nets
{
    public class Matrix // : IEquatable<Matrix>
    {
        double[,] elements;
        public int Columns => elements.GetLength(1);
        public int Rows => elements.GetLength(0);
        public double this[int rowNr, int columnNr]
        {
            get { return elements[rowNr, columnNr]; }
            set { elements[rowNr, columnNr] = value; }
        }

        public Matrix(int Rows, int Columns)
        {
            elements = new double[Rows, Columns];
        }

        public Matrix(double[,] list)
        {
            elements = list;
        }

        static void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp = lhs;
            lhs = rhs;
            rhs = temp;
        }

        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            if (m1.Rows != m2.Columns)
                throw new ArgumentException(nameof(m1) + " " + nameof(m2));

            Matrix temp = new Matrix(m1.Rows, m2.Columns);
            for (int i = 0; i < temp.Rows; i++)
            {
                for (int j = 0; j < temp.Columns; j++)
                {
                    for (int k = 0; k < m2.Rows; k++)
                    {
                        temp[i, j] = temp[i, j] + m1[i, k] * m2[k, j];
                    }
                }
            }
            return temp;
        }

        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            Matrix temp = new Matrix(m1.Rows, m1.Columns);
            if (m1.Rows == m2.Rows && m1.Columns == m2.Columns)
            {
                for (int i = 0; i < m1.Rows; i++)
                {
                    for (int j = 0; j < m1.Columns; j++)
                    {
                        temp[i, j] = m1[i, j] + m2[i, j];
                    }
                }
                return temp;
            }
            throw new ArgumentException(nameof(m1) + " " + nameof(m2));
        }

        public Matrix SumByLength(Matrix m1) //TODO
        {
            Matrix tmp = new Matrix(m1.Rows, 1);
            for (int i = 0; i < m1.Columns; i++)
            {
                for (int j = 0; j < m1.Rows; j++)
                {
                    tmp[j, 0] += m1[i, j];
                }
            }
            return tmp;
        }

        public Matrix SumByWidth(Matrix m1) //TODO
        {
            Matrix tmp = new Matrix(1, m1.Columns);
            for (int i = 0; i < m1.Rows; i++)
            {
                for (int j = 0; j < m1.Columns; j++)
                {
                    tmp[0, j] += m1[j, i];
                }
            }
            return tmp;
        } 
        public double[] ConvertToArray()
        {
            if (Rows != 1 && Columns != 1)
                throw new ArgumentException("Matrix must have at least one dimension of size equal 1");
            double[] temp = new double[Math.Max(Columns, Rows)];
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    temp[j + i] = elements[i, j];
                }
            }
            return temp;
        }

        public void Populate(Func<int, int, double> populatingFunction)
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    elements[i, j] = populatingFunction(i, j);
                }
            }
        }

        public void ApplyFunction(Func<double, double> appliedFunction)
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    elements[i, j] = appliedFunction(elements[i, j]);
                }
            }
        }

        public void Print() //Temporary
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Console.Write(elements[i, j].ToString() + "\t");
                }
                Console.WriteLine();
            }
        }

        public override string ToString()
        {
            return base.ToString();
        }

    }
}
