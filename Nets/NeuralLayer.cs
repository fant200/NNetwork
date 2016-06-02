using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    class NeuralLayer
    {
        Matrix weights,biases;
        NeuralLayer prevLayer;
        Matrix errors,deltas;
        Matrix layerInput, layerOutput;
        int Size => weights.Rows;
        int Inputs => weights.Columns;

        NeuralLayer(int layerSize, int numOfInputs)
        {
            weights = new Matrix(layerSize, numOfInputs);
            errors = new Matrix(layerSize, 1);
            deltas = new Matrix(layerSize, 1);
            layerInput = new Matrix(layerSize, 1);
        }
        NeuralLayer(int layerSize, NeuralLayer _prevLayer): this(layerSize, _prevLayer.Size)
        {

        }

    }
}
