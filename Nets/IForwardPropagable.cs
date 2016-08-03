using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nets
{
    interface IForwardPropagable
    {
        Matrix ForwardPropagate(Matrix input);
    }
}
