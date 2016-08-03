using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nets
{
    public class NeuralNetwork
    {
        public NeuralLayer[] layers;
        public readonly int inputsNr;
        Matrix netInput;
        public int LayersNum => layers.Length;
        public Matrix Error => layers.Last().Errors;

        public NeuralNetwork(int inputs)
        {
            netInput = new Matrix(inputs, 1);
            inputsNr = inputs;
        }

        public Matrix ForwardPropagate(Matrix input)
        {
            Matrix temp = input;
            for (int i = 0; i < LayersNum; i++)
            {
                temp = layers[i].ForwardPropagate(temp);
            }
            return temp;
        }

    }
}
