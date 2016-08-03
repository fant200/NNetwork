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
            Console.ReadKey();
            Matrix a = new Matrix(1, 2);
            Matrix b = new Matrix(2, 1);
            Matrix c;
            c = a - b;
            Console.ReadKey();
        }
    }
}
