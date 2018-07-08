using Accord.Math;
using NeuralNetworks.Core.Layers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimization
{
    public class Regularization
    {
        public double L2<T>(double lambda, int amountOfExamples, List<T> layers) where T : ILayer
        {
            double sumOfWeights = 0;
            for (int i = 1; i < layers.Count; i++)
            {
                sumOfWeights += layers[i].Weights.Multiply(layers[i].Weights).Sum();
            }

            return lambda / (2.0 * amountOfExamples) * sumOfWeights;
        }
    }
}
