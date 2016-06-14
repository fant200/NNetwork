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
        Matrix errors, deltas;
        Matrix layerOutput;
        public static Random rng = new Random();
        int Size => weights.Rows;
        int Inputs => weights.Columns;

        public NeuralLayer(int numOfInputs, int layerSize)
        {
            weights = new Matrix(layerSize, numOfInputs);
            weights.ForEach(x => rng.NextDouble());
            biases = new Matrix(layerSize, 1);
            biases.ForEach(x => 1);
        }
        public NeuralLayer(int layerSize, NeuralLayer _prevLayer) : this(_prevLayer.Size, layerSize)
        {
            prevLayer = _prevLayer;
        }

        public Matrix ForwardPropagate(Matrix input)
        {
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

        public Matrix CalculateSelfError(Matrix targets)
        {
            errors = layerOutput * targets;
            return errors;
        }

        public void SetPrevLayerError()
        {
            prevLayer.errors = CalculatePrevLayerError();
        }

        public Matrix CalculatePrevLayerError()
        {
            return errors * weights;
        }


    }
}
