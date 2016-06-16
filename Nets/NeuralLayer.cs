using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nets
{
    class NeuralLayer
    {
        public Matrix weights, biases;
        NeuralLayer prevLayer;
        Matrix deltas, errors;
        Matrix momentum;
        Matrix layerOutput, layerInput;
        double learningMultiplier = 0.4;
        public static Random rng = new Random();
        int Size => weights.Rows;
        int Inputs => weights.Columns;

        public NeuralLayer(int numOfInputs, int layerSize)
        {
            weights = new Matrix(layerSize, numOfInputs);
            weights.ForEach(x => rng.NextDouble());
            momentum = new Matrix(layerSize, numOfInputs);
            biases = new Matrix(layerSize, 1);
            biases.ForEach(x => 1);
        }
        public NeuralLayer(int layerSize, NeuralLayer _prevLayer) : this(_prevLayer.Size, layerSize)
        {
            prevLayer = _prevLayer;
        }

        public Matrix ForwardPropagate(Matrix input)
        {
            layerInput = input;
            layerOutput = weights * input;
            AddBiases();
            layerOutput.ForEach(Math.Tanh);
            return layerOutput;
        }

        private void AddBiases()
        {
            for (int i = 0; i < layerOutput.Rows; i++)
            {
                for (int j = 0; j < layerOutput.Columns; j++)
                {
                    layerOutput[i, j] += biases[i, 0];
                }
            }
        }

        public void CalculateDelta(Matrix targets)
        {
            Matrix temp = layerOutput.DeepCopy();
            temp.ForEach(x => 1 - x * x);

            errors = (layerOutput - targets);

            deltas = Matrix.EntitywiseMul(errors, temp);
        }

        public void CalculateDelta()
        {
            Matrix temp = layerOutput.DeepCopy();
            temp.ForEach(x => 1 - x * x);

            deltas = Matrix.EntitywiseMul(errors, temp);
        }

        public void BackpropError()
        {
            prevLayer.errors = weights.Transpose() * deltas;
        }

        public void UpdateWeights()
        {
            momentum *= 0.45;
            momentum += deltas * layerInput.Transpose() * learningMultiplier;
            weights -= momentum;
        }
    }
}
