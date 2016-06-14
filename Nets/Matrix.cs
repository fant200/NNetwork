using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Nets
{
    [DebuggerDisplay("Matrix rows={Rows}, columns={Columns}")]

    public class Matrix : IEquatable<Matrix>
    {
        double[,] elements;
        public int Rows => elements.GetLength(0);
        public int Columns => elements.GetLength(1);
        public double this[int rowNr, int columnNr]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return elements[rowNr, columnNr]; }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { elements[rowNr, columnNr] = value; }
        }

        public Matrix(int Rows, int Columns)
        {
            elements = new double[Rows, Columns];
        }

        public Matrix(double[,] array) : this(array.GetLength(0), array.GetLength(1))
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    elements[i, j] = array[i, j];
                }
            }
        }

        public Matrix EntitywiseMul(Matrix m1, Matrix m2)
        {
            if (m1.Columns != m2.Rows)
                throw new ArgumentException(nameof(m1) + " " + nameof(m2) + " have wrong dimensions");

            Matrix temp = new Matrix(m1.Rows, m1.Columns);
            for (int i = 0; i < m1.Rows; i++)
            {
                for (int j = 0; j < m1.Columns; j++)
                {
                    temp[i, j] = m1[i, j] * m2[i, j];
                }
            }

            return temp;
        }

        public Matrix Transpose()
        {
            Matrix temp = new Matrix(Columns, Rows);

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    temp[j, i] = this[i, j];
                }
            }

            return temp;
        }

        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            if (m1.Columns != m2.Rows)
                throw new ArgumentException(nameof(m1) + " " + nameof(m2) + " have wrong dimensions");

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

        public static Matrix operator *(double d1, Matrix m1)
        {
            return m1 * d1;
        }
        public static Matrix operator *(Matrix m1, double d1)
        {
            Matrix temp = new Matrix(m1.Rows, m1.Columns);
            for (int i = 0; i < m1.Rows; i++)
            {
                for (int j = 0; j < m1.Columns; j++)
                {
                    temp[i, j] = m1[i, j] * d1;
                }
            }
            return temp;
        }

        public static Matrix operator *(int i1, Matrix m1)
        {
            return m1 * i1;
        }
        public static Matrix operator *(Matrix m1, int i1)
        {
            Matrix temp = new Matrix(m1.Rows, m1.Columns);
            for (int i = 0; i < m1.Rows; i++)
            {
                for (int j = 0; j < m1.Columns; j++)
                {
                    temp[i, j] = m1[i, j] * i1;
                }
            }
            return temp;
        }

        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            if (m1.Rows != m2.Rows || m1.Columns != m2.Columns)
                throw new ArgumentException(nameof(m1) + " " + nameof(m2) + " matrices must have equal dimensions");

            Matrix temp = new Matrix(m1.Rows, m1.Columns);

            for (int i = 0; i < m1.Rows; i++)
            {
                for (int j = 0; j < m1.Columns; j++)
                {
                    temp[i, j] = m1[i, j] + m2[i, j];
                }
            }
            return temp;
        }

        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            if (m1.Rows != m2.Rows || m1.Columns != m2.Columns)
                throw new ArgumentException(nameof(m1) + " " + nameof(m2) + " matrices must have equal dimensions");

            Matrix temp = new Matrix(m1.Rows, m1.Columns);

            for (int i = 0; i < m1.Rows; i++)
            {
                for (int j = 0; j < m1.Columns; j++)
                {
                    temp[i, j] = m1[i, j] - m2[i, j];
                }
            }
            return temp;
        }
        public bool Equals(Matrix other)
        {
            if (Rows == other.Rows && Columns == other.Columns)
            {
                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Columns; j++)
                    {
                        if (Math.Abs(this[i, j] - other[i, j]) > double.Epsilon)
                            return false;
                    }
                }
                return true;

            }
            return false;
        }

        public static bool operator ==(Matrix m1, Matrix m2)
        {
            if (m1 == null)
                return false;
            return m1.Equals(m2);
        }

        public static bool operator !=(Matrix m1, Matrix m2)
        {
            return !(m1 == m2);
        }
        public static explicit operator double[,] (Matrix m1)
        {
            return m1.elements;
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

        public Matrix SumByWidth(Matrix m1)
        {
            //TODO : SumByWidth
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

        public Matrix DeepCopy()
        {
            return new Matrix(elements);
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            var comp = obj as Matrix;
            if (comp == null)
                return false;
            return Equals(comp);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder((Rows + 2) * (Columns + 2));
            for (int i = 0; i < Rows; ++i)
            {
                builder.Append('-');
            }
            for (int i = 0; i < Rows; ++i)
            {
                builder.Append('|');
                for (int j = 0; j < Columns; ++j)
                {
                    builder.Append(this[i, j]).Append(' ');
                }
                builder.Append('|');
            }
            return builder.ToString();

        }

    }
}
