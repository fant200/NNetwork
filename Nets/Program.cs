using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nets
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            NNetwork a = new NNetwork(2, new int[] { 4, 1 });
            Matrix input = new Matrix(new double[,] { { 0, 0, 1, 1 }, { 0, 1, 0, 1 } });
            Matrix targets = new Matrix(new double[,] { { 0, 1, 1, 0 } });
            Console.WriteLine(a.ForwardPropagate(input));
            for (int i = 0; i < 1000; i++)
            {
                a.TrainingPass(input, targets);
            }
            Console.WriteLine(a.ForwardPropagate(input));
            Console.ReadKey();
        }
    }
}
