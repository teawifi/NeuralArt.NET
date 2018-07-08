using Accord.Math;
using MathCore.ActivationFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimization.CostFunction
{
    public class CrossEntropy : ICostFunction
    {
        /*
         * C=−1/n*∑x∑j[yjlnaLj+(1−yj)ln(1−aLj)]. 
         */
        public double Function(int[,] desired, double[,] actual)
        {
            double sum = 0;

            for (int i = 0; i < actual.GetLength(0); i++)
                sum += Loss.Entropy(desired.GetRow(i), actual.GetRow(i)).Sum();

            return 1.0 / actual.GetLength(0) * sum;
        }

        public double Function(int[,] desired, List<double[]> actual)
        {
            double sum = 0;

            for (int i = 0; i < actual.Count; i++)
                sum += Loss.Entropy(desired.GetRow(i), actual[i]).Sum();

            return 1.0 / actual.Count * sum;
        }

        public double[] Delta(int[] desired, double[] actual, IActivationFunction activation)
        {
            return actual.Subtract(desired);
        }
    }
}
