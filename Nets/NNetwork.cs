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
        public Matrix Error => layers.Last().Errors;

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

        void CalculateDeltas(Matrix targets)
        {
            layers.Last().CalculateDelta(targets);
            layers.Last().BackpropError();

            for (int i = layers.GetUpperBound(0); i > 1; i--)
            {
                layers[i].CalculateDelta();
                layers[i].BackpropError();
            }

            layers[0].CalculateDelta();
        }

        void UpdateWeights()
        {
            for (int i = 0; i < LayersNum; i++)
            {
                layers[i].UpdateWeights();
            }
        }

        public void BackPropagate(Matrix targets)
        {
            CalculateDeltas(targets);
            UpdateWeights();
        }

        public void TrainingPass(Matrix inputs, Matrix targets)
        {
            ForwardPropagate(inputs);
            BackPropagate(targets);
        }
    }
}
