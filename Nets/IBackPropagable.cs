using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nets
{
    interface IBackPropagable
    {
        Matrix Errors { get; }
        Matrix BackPropagate(Matrix input);
        void Update();
    }
}
