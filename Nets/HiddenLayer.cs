using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nets
{
    public class HiddenLayer : FeedForwardLayer
    {
        public HiddenLayer(int numOfInputs, int layerSize) : base(numOfInputs, layerSize)
        {
        }

        protected override Matrix CalculateErrors(Matrix nextLayerError)
        {
            Matrix outputDerivatives;
            outputDerivatives = layerOutput.DeepCopy();
            outputDerivatives.ForEach(x => 1 - x * x);
            return Matrix.EntitywiseMul(nextLayerError, outputDerivatives);
        }

    }
}
