using NeuralNetworks.Layers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks.Optimizers
{
    public interface IOptimizer
    {
        void Run(List<Dense> layers, double learningRate, double lambda, int amountOfExamples);
    }
}
