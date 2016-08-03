using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nets
{
    public abstract class FeedForwardLayer : NeuralLayer
    {
        protected Matrix weights,biases;
        protected Matrix errors, deltas;

        public override int OutputSize => weights.Rows;
        public override int InputSize => weights.Columns;
        public override Matrix Errors => errors;

        protected FeedForwardLayer(int numOfInputs, int layerSize) : base(numOfInputs, layerSize)
        {
            weights = new Matrix(layerSize, numOfInputs);
            weights.ForEach(x => rng.NextDouble());
            biases = new Matrix(layerSize, 1);
            biases.ForEach((x) => 1);
        }

        public override Matrix BackPropagate(Matrix input)
        {
            errors = CalculateErrors(input);
            deltas = CalculateDeltas(errors);
            return weights.Transpose() * errors;
        }

        protected Matrix CalculateDeltas(Matrix error)
        {
            return Matrix.EntitywiseMul(error, layerInput);
        }
        protected abstract Matrix CalculateErrors(Matrix input);

        protected override Matrix CalculateOutput(Matrix input)
        {
            layerInput = input;
            Matrix output;
            output = weights * input;
            output.ForEach((i, j, x) => x + biases[i, 0]); // Add biases
            output.ForEach(x => Math.Tanh(x));
            deltas = new Matrix(OutputSize, input.Columns);
            return output;
        }
        public override void Update()
        {
            weights -= deltas * learningRate;
        }
    }
}
