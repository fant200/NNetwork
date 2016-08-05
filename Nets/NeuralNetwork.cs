using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nets
{
    public class NeuralNetwork
    {
        private List<NeuralLayer> layers;
        public readonly int inputsNr;
        Matrix netInput;
        public int LayersNum => layers.Count;
        public Matrix Error => layers.Last().Errors;

        protected NeuralNetwork(int inputs, List<NeuralLayer> buildedLayers)
        {
            inputsNr = inputs;
            layers = buildedLayers;
        }

        public Matrix ForwardPropagate(Matrix input)
        {
            netInput = input;
            Matrix temp = input;
            for (int i = 0; i < LayersNum; i++)
            {
                temp = layers[i].ForwardPropagate(temp);
            }
            return temp;
        }

        public class Builder
        {
            int networkInputs;
            List<NeuralLayer> layers = new List<NeuralLayer>();
            Tuple<string, int> prevAdded;

            public Builder(int NetworkInputs)
            { networkInputs = NetworkInputs; }

            public void Add(string Type, int size)
            {
                switch (Type.ToLower())
                {
                    case ("forward"):
                        if (prevAdded != null)
                            AppendLayer(prevAdded);
                        prevAdded = new Tuple<string, int>(Type, size);
                        break;
                }
            }

            void AppendLayer(Tuple<string, int> layerToken)
            {
                switch (layerToken.Item1)
                {
                    case ("forward"):
                        if (layers.Count == 0)
                            layers.Add(new HiddenLayer(networkInputs, layerToken.Item2));
                        else
                            layers.Add(new HiddenLayer(layers.Last().OutputSize, layerToken.Item2));
                        break;

                }
            }

            public NeuralNetwork Build()
            {
                layers.Add(new OutputLayer(layers.Last().OutputSize, prevAdded.Item2));
                return new NeuralNetwork(networkInputs, layers);
            }
        }
    }
}
