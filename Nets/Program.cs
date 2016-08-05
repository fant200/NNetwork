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
            NeuralNetwork a;
            NeuralNetwork.Builder builder = new NeuralNetwork.Builder(1);
            builder.Add("forward", 10);
            builder.Add("forward", 1);
            a = builder.Build();
            Console.ReadKey();
        }
    }
}
