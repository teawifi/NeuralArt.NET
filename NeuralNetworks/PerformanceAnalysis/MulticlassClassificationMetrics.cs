using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks.PerformanceAnalysis
{
    public class MulticlassClassificationMetrics
    {
        public double Accuracy(int[] predicted, int[] expected)
        {
            int correctlyClassified = 0;
            for (int i = 0; i < expected.Length; i++)
            {
                if (predicted[i] == expected[i])
                    correctlyClassified++;
            }

            return correctlyClassified / (double)expected.Length;
        }       
    }
}
