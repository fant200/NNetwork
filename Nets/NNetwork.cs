using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nets
{
    public class NNetwork
    {
        NeuralLayer[] layers;
        readonly int inputsNr;
        Matrix netInput;
        int LayersNum => layers.Length;

        public NNetwork(int inputs, int[] NetworkComposition)
        {
            if (NetworkComposition.Length < 1)
                throw new ArgumentException("Neural Network must have minimum 1 layer");

            netInput = new Matrix(inputs, 1);
            inputsNr = inputs;
            layers = new NeuralLayer[NetworkComposition.Length];

            layers[0] = new NeuralLayer(inputs, NetworkComposition[0]);
            for (int i = 1; i < NetworkComposition.Length; i++)
            {
                layers[i] = new NeuralLayer(NetworkComposition[i], layers[i - 1]);
            }
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
