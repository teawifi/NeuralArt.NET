using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks.PerformanceAnalysis
{
    public class BinaryClassificationMetrics   
    {
        //  2x2 error matrix
        //              Actual class
        //                1     0
        // Predicted 1 | TP    FP |
        //             |          |
        //  class    0 | FN    TN |
        // 

        private int truePositives;  // TP
        private int falseNegatives; // FN
        private int falsePositives; // FP
        private int trueNegatives;  // TN

        public BinaryClassificationMetrics(int[] predicted, int[] expected, int positiveValue = 1)
        {
            Compute(predicted, expected, positiveValue);
        }

        private void Compute(int[] predicted, int[] expected, int positiveValue)
        {
            for (int i = 0; i < expected.Length; i++)
            {
                if (predicted[i] == expected[i])
                {
                    if (predicted[i] == positiveValue)
                        truePositives++;
                    else
                        trueNegatives++;
                }
                else
                {
                    if (predicted[i] == positiveValue)
                        falsePositives++;
                    else
                        falseNegatives++;
                }
            }
        }

        public double Accuracy
        {
            get { return (truePositives + trueNegatives) / (double)NumberOfSamples; }
        }

        public double Precision
        {
            get { return truePositives / (truePositives + falsePositives); }
        }

        public double Recall
        {
            get { return truePositives / (truePositives + falseNegatives); }
        }
        
        public double FScore
        {
            get { return 2 * Precision * Recall / (Precision + Recall); }
        }

        public double FalsePositiveRate
        {
            get { return falsePositives / (falsePositives + trueNegatives); }
        }

        public double TruePositiveRate
        {
            get { return truePositives / (truePositives + falseNegatives); }
        }

        public int NumberOfSamples
        {
            get
            {
                return truePositives + trueNegatives + falseNegatives + falsePositives;
            }
        }
    }
}
