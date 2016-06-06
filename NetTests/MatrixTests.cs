#define TESTUJE
using System;
using Nets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetTests
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Security.Principal;

    [TestClass]
    public class MatrixTests
    {
        private Random random = new Random(DateTime.Now.Millisecond ^ 2);

        [TestMethod]
        public void StaticCheck()
        {
            var type = typeof(Matrix);
            var fullName = type.Namespace + "." + type.Name;
            var expectedMethods = new Dictionary<string, Tuple<string, List<List<string>>>>
            {
                {
                    "op_Multiply",
                    new Tuple<string, List<List<string>>>
                    ($"{type.FullName}",
                   new List<List<string>>
                    {
                        new List<string> {type.FullName, type.FullName},
                        new List<string> {type.FullName, typeof (double).FullName},
                        new List<string> {typeof (double).FullName, type.FullName},
                        new List<string> {type.FullName, typeof (int).FullName},
                        new List<string> {typeof (int).FullName, type.FullName},
                    }
                        )
                },
                {
                    "op_Addition",
                    new Tuple<string, List<List<string>>>
                    ($"{type.FullName}",
                   new List<List<string>>
                    {
                        new List<string> {type.Name, type.Name},
                    }
                        )
                },
                {
                    "op_Equality",
                    new Tuple<string, List<List<string>>>
                    ($"{typeof (bool).FullName}",
                    new List<List<string>>
                    {
                        new List<string> {type.Name,type.Name},
                    }
                        )
                },
                {
                    "op_Inequality",
                    new Tuple<string, List<List<string>>>
                    ($"{typeof (bool).FullName}",
                    new List<List<string>>
                    {
                        new List<string> {type.Name,type.Name},
                    }
                        )
                },
            };
            var methods = type.GetMethods(BindingFlags.Static | BindingFlags.Public).Select(x => x.GetFullName()).ToList();
            Func<string, string, string, IEnumerable<string>, string> lambda = (assemblyName, typeName, methodName, args)
                => $"{typeName} {methodName}({string.Join(",", args)})";
            foreach (var key in expectedMethods.Keys)
            {
                foreach (var list in expectedMethods[key].Item2)
                {
                    var str = lambda(type.Namespace, expectedMethods[key].Item1, key, list);
                    Assert.IsTrue(methods.Contains(str), str);
                }
            }
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MulMatrixWithWrongDim()
        {
            Matrix a = new Matrix(3, 3);
            Matrix b = new Matrix(4, 4);
            a = a * b;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TooBigMatrix()
        {
            new Matrix(100000, 100000); // ciezko zmiescic w ramie a "OutOfMemoryException" nie sugeruje ze probujemy zaalokowac za duza tablice stad argument exception
        }
        [TestMethod]
        public void MatrixInit()
        {
            int width = random.Next(0, 30);
            int height = random.Next(0, 30);
            var matrix = new Matrix(height, width);
            var data = new double[height, width];
            data.Randomize();
            var matrix2 = new Matrix(data);
            Action<Matrix, double[,], int, int> fill = (matrix1, doubles, arg3, arg4) => matrix1[arg3, arg4] = doubles[arg3, arg4];
            DoActionInLoop(matrix, data, fill);
            AreEqual(matrix, data);
            AreEqual(matrix2, data);
        }

        [TestMethod]
        public void TheSameArrays()
        {
            var arr = new double[,] { { 1, 2 }, { 3, 4 } };
            var matrix = new Matrix(arr);
            var fieldsArray = typeof(Matrix).GetAllFields();
            var field = fieldsArray.FirstOrDefault(x => x.FieldType == typeof(double[,]));
            double[,] fieldD =
                matrix.GetFieldValue(field).As<double[,]>();
            arr[0, 0] = 30;
            if (fieldD != null)
            {
                Assert.AreNotSame(arr, fieldD); // nie moga miec tych samych referencji bo jak ktos zmieni poczatkowa tablice po za macierza to macierz sie zmieni
                Assert.AreNotEqual(arr[0, 0], matrix[0, 0]);
            }
            else
            {
                Assert.Fail("Cos nie dziala");
            }
        }
        [TestMethod]
        public void PopulateTest()
        {
            var matrix = new Matrix(10, 10);
            Func<int, int, double> lambda = (i, j) => (i + j);
            matrix.Populate(lambda);
            DoActionInLoop(matrix, (m, i, j) => Assert.AreEqual(lambda(i, j), m[i, j]));
        }
        #region operators
        [TestMethod]
        public void Sum()
        {
            var arr = new double[,]
            {
                {1, 2, 3, 4, 5},
                {6, 7, 8, 9, 10 },
                {11,12,13,14,15},
            };
            var arr2 = new double[,]
            {
                {1, 2, 3, 4, 5},
                {6, 7, 8, 9, 10},
                {11, 12, 13, 14, 15},
            };
            var matrix = new Matrix(arr);
            var matrix2 = new Matrix(arr2);
            var matrix3 = matrix + matrix2;
            DoActionInLoop(matrix3, arr, arr2,
                (matr, doubles1, doubles2, i, j) =>
                Assert.AreEqual(doubles1[i, j] + doubles2[i, j], matr[i, j])
                );

        }

        [TestMethod]
        public void ExplicitOrImplicitCastToArray()
        {
#if TESTUJE
            var matrix = new Matrix(10, 0);
            double[,] tmp = (double[,])( matrix );
            if (tmp == null)
                Assert.Fail();
#endif
        }

        #endregion





        private void AreEqual(Matrix matrix, double[,] arr)
        {
            DoActionInLoop(matrix, arr,
                   (matrix1, doubles, arg3, arg4) => Assert.AreEqual(doubles[arg3, arg4], matrix[arg3, arg4]));
        }
        private void DoActionInLoop(Matrix matrix, double[,] arr, Action<Matrix, double[,], int, int> action)
        {
            Assert.AreEqual(arr.GetLength(1), matrix.Columns);
            Assert.AreEqual(arr.GetLength(0), matrix.Rows);
            for (int i = 0; i < arr.GetLength(0); ++i)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    action.Invoke(matrix, arr, i, j);
                }
            }
        }
        private void DoActionInLoop(Matrix matrix, double[,] arr, double[,] arr2, Action<Matrix, double[,], double[,], int, int> action)
        {
            Assert.AreEqual(arr.GetLength(1), matrix.Columns);
            Assert.AreEqual(arr.GetLength(0), matrix.Rows);
            for (int i = 0; i < arr.GetLength(0); ++i)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    action.Invoke(matrix, arr, arr2, i, j);
                }
            }
        }
        private void DoActionInLoop(Matrix matrix, Action<Matrix, int, int> action)
        {
            for (int i = 0; i < matrix.Rows; ++i)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    action.Invoke(matrix, i, j);
                }
            }
        }
    }
}
