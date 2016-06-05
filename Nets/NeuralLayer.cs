using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nets
{
    class NeuralLayer
    {
        public Matrix weights,biases;
        NeuralLayer prevLayer;
        Matrix errors,deltas;
        Matrix layerOutput;
        public static Random rng = new Random();
        int Size => weights.Rows;
        int Inputs => weights.Columns;

        public NeuralLayer(int numOfInputs, int layerSize)
        {
            weights = new Matrix(layerSize, numOfInputs);
            weights.ApplyFunction(x => rng.NextDouble());
            biases = new Matrix(layerSize, 1);
            biases.ApplyFunction(x => 1);
            errors = new Matrix(layerSize, 1);
            deltas = new Matrix(layerSize, 1);
            layerOutput = new Matrix(layerSize, 1);
        }
        public NeuralLayer(int layerSize, NeuralLayer _prevLayer): this(layerSize, _prevLayer.Size)
        {
            prevLayer = _prevLayer;
        }

        public Matrix ForwardPropagate(Matrix input)
        {
            layerOutput = weights * input;
            layerOutput += biases;
            layerOutput.ApplyFunction(Math.Tanh);
            return layerOutput;
        }

        public Matrix CalculateSelfError(Matrix targets)
        {
            errors = layerOutput * targets;
            return errors;
        }

        public Matrix CalculatePrevLayerError()
        {
            return errors * weights;
        }


    }
}
