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
        public int Columns => elements.GetLength(1);
        public int Rows => elements.GetLength(0);
        public double this[int rowNr,int columnNr]
        {
            get { return elements[rowNr,columnNr]; }
            set { elements[rowNr,columnNr] = value; }
        }

        public Matrix()
        { }
        public Matrix(int Rows,int Columns)
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

        public void CopyResize(int newRowsNr, int newColumnsNr)
        {
            double[,] tmp = elements;
            int oldRowsNr, oldColumnsNr;
            oldColumnsNr = Columns;
            oldRowsNr = Rows;

            elements = new double[newRowsNr, newColumnsNr];
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    elements[i, j] = tmp[i % oldRowsNr, j % oldColumnsNr];
                }
            }
        }

        public static Matrix operator* (Matrix m1, Matrix m2)
        {
            Matrix temp = new Matrix(Math.Max(m1.Rows,m2.Rows), Math.Max(m1.Columns,m2.Columns));
            Console.WriteLine(temp.Columns);
            Console.WriteLine(temp.Rows);
            Console.WriteLine(m1.Rows.ToString() + " " + m1.Columns.ToString());
            
            if ((m1.Rows == 1 ^ m2.Rows == 1) && (m1.Columns == 1 ^ m2.Columns == 1))
            {
                Matrix longer = m2;
                Matrix wider = m2;
                if (m1.Columns > m2.Columns)
                    wider = m1;
                if (m1.Rows > m2.Rows)
                    longer = m1;

                for (int i = 0; i < temp.Rows; i++)
                {
                    for (int j = 0; j < temp.Columns; j++)
                    {
                        temp[i, j] = wider[0, j] * longer[i, 0];
                    }
                }
                return temp;
            }
            else if (m1.IsSameSize(m2))
            {
                for (int i = 0; i < temp.Rows; i++)
                {
                    for (int j = 0; j < temp.Columns; j++)
                    {
                        temp[i, j] = m1[i, j] * m2[i, j];
                    }
                }
                return temp;
            }
            else if(temp.IsSameSize(m1) ^ temp.IsSameSize(m2))
            {
                Matrix smaller = m2;
                Matrix bigger = m1;
                if (m2.IsSameSize(temp))
                {
                    smaller = m1;
                    bigger = m2;
                }
                if(smaller.Rows == temp.Rows && smaller.Columns == 1)
                {
                    for (int i = 0; i < temp.Rows; i++)
                    {
                        for (int j = 0; j < temp.Columns; j++)
                        {
                            temp[i, j] = bigger[i, j] * smaller[i, 0]; 
                        }
                    }
                    return temp;
                }
                if(smaller.Columns == temp.Columns && smaller.Rows==1)
                {
                    for (int i = 0; i < temp.Rows; i++)
                    {
                        for (int j = 0; j < temp.Columns; j++)
                        {
                            temp[i, j] = bigger[i, j] * smaller[0, j];
                        }
                    }
                    return temp;
                }
            }
            throw new NotImplementedException();    
        }

        public static Matrix operator+(Matrix m1, Matrix m2)
        {
            Matrix temp = new Matrix(m1.Rows, m1.Columns);
            if(m1.Rows == m2.Rows && m1.Columns == m2.Columns)
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
            throw new NotSupportedException();
        }

        public Matrix SumByLength(Matrix m1)
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

        public Matrix SumByWidth(Matrix m1)
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

        public bool IsSameSize(Matrix m1)
        {
            return (Rows == m1.Rows && Columns == m1.Columns);
        }

        public void Print()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Console.Write(elements[i, j].ToString() + " ");
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
