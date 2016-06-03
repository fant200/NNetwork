using System;
using Nets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetTests
{
    [TestClass]
    public class UnitTest1
    {
        static Random rng = new Random();

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MulMatrixWithWrongDim()
        {
            Matrix a = new Matrix(3, 3);
            Matrix b = new Matrix(4, 4);
            a = a * b;
        }
    }
}
