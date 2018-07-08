using Accord.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCore.ActivationFunctions
{
    public class Sigmoid : IActivationFunction
    {
        public double[] Function(double[] z)
        {
            double[] activatedOutputs = new double[z.Length];

            for (var i = 0; i < z.Length; i++)
            {
                activatedOutputs[i] = 1.0 / (1 + Math.Exp(-z[i]));
            }

            return activatedOutputs;
        }

        public double[] PartialDerivative(double[] activatedOutputs)
        {
            return Elementwise.Multiply(activatedOutputs, 1.Subtract(activatedOutputs));
        }
    }
}
