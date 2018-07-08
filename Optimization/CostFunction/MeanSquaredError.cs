using Accord.Math;
using MathCore.ActivationFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimization.CostFunction
{
    public class MeanSquaredError : ICostFunction
    {
        public double Function(int[,] desired, List<double[]> actual)
        {
            double sum = 0;

            for (int i = 0; i < actual.Count; i++)
                sum += Loss.SquaredLoss(desired.GetRow(i), actual[i]).Sum();

            return 1.0 / (2 * actual.Count) * sum;
        }

        public double Function(int[,] desired, double[,] actual)
        {
            double sum = 0;

            for (int i = 0; i < actual.GetLength(0); i++)
                sum += Loss.SquaredLoss(desired.GetRow(i), actual.GetRow(i)).Sum();

            return 1.0 / (2 * actual.GetLength(0)) * sum;
        }

        // calculate error of output layer
        // δ^(L)=∇aC ⊙ σ′(z^(L))
        public double[] Delta(int[] desired, double[] actual, IActivationFunction activation)
        {
            double[] gradient = actual.Subtract(desired);
            var sigmoidDerivative = Elementwise.Multiply(actual, 1.Subtract(actual));

            return Elementwise.Multiply(gradient, sigmoidDerivative);            
        }        
    }
}
