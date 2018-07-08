using Accord.Math;
using NeuralNetworks.Layers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks.Optimizers
{
    public class GradientDescent : IOptimizer
    {
        public void Run(List<Dense> layers, double learningRate, double lambda, int amountOfExamples)
        {
            /*
             * Gradient descent: For each l = L,L−1,…,2 update the weights according to the rule wl→wl−η/m∑xδx,l(ax, l−1)T, 
             * and the biases according to the rule bl→bl−ηm∑xδx,l.
             */

            for (int i = 1; i < layers.Count; i++)
            {
               
                var temp = layers[i].WeightGradients.Multiply(learningRate);
                var reg = (1 - learningRate * lambda / amountOfExamples).Multiply(layers[i].Weights);
                layers[i].Weights = reg.Subtract(temp);
               
                double r = learningRate / amountOfExamples;
                layers[i].Biases = layers[i].Biases.Subtract(layers[i].ErrorsSum.Multiply(r));                
            }
        }
    }
}
