using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks.Core.Layers
{
    public interface ILayer
    {          
        double[,] Weights { get; set; } //weights of a layer       
    }
}
