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
            double[,] list = { { 1, 2, 3, 4 } };
            NNetwork net = new NNetwork(1, new int[] { 4, 1 });
            double[,] tempinput = { {-1, 0, 1 }};
            Matrix input = new Matrix(tempinput);
            net.ForwardPropagate(input).Print();
            Console.ReadKey();
        }
    }
}
