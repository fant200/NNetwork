using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nets
{
    class OutputLayer : FeedForwardLayer
    {
        public OutputLayer(int numOfInputs, int layerSize) : base(numOfInputs, layerSize)
        {
        }

        protected override Matrix CalculateErrors(Matrix targets)
        {
            return layerOutput - targets;
        }
    }
}
