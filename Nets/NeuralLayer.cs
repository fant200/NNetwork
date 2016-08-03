using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nets
{
    public abstract class NeuralLayer : IForwardPropagable, IBackPropagable
    {
        protected Matrix layerInput, layerOutput;
        protected static readonly Random rng;
        public double learningRate = 0.05;

        static NeuralLayer()
        {
            rng = new Random(); //Chcemy kontrolować kiedy to się stworzy
        }

        public abstract int OutputSize { get; }
        public abstract int InputSize { get; }
        public abstract Matrix Errors { get; }

        protected NeuralLayer(int numOfInputs, int layerSize) { }

        public Matrix ForwardPropagate(Matrix input)
        {
            layerInput = input;
            layerOutput = CalculateOutput(layerInput);
            return layerOutput;
        }

        protected abstract Matrix CalculateOutput(Matrix input);
        public abstract Matrix BackPropagate(Matrix input);
        public abstract void Update();
    }
}
