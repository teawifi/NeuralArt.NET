using Accord.Statistics.Distributions.Multivariate;
using Accord.Statistics.Distributions.Univariate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCore
{
    public static class Statistics
    {
        public static double Mean(this double[] vector)
        {
            if (vector == null)
                throw new ArgumentNullException("vector");

            return vector.Sum() / vector.Count();
        }

        public static double[] Mean(this double[,] matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException("matrix");

            int examples = matrix.GetLength(0);
            int features = matrix.GetLength(1);

            double[] averageValues = new double[features];

            double[] sumArray = matrix.Sum(dimension: 0);

            for (int i = 0; i < features; i++)
            {
                averageValues[i] = sumArray[i] / examples;
            }

            return averageValues;
        }

        public static double[] StandardDeviation(this double[,] data, double[] averageValues)
        {
            var numberOfExamples = data.GetLength(0);
            var standardDeviations = new double[data.GetLength(1)];
            for (int feature = 0; feature < data.GetLength(1); feature++)
            {
                double sum = 0;
                for (int example = 0; example < numberOfExamples; example++)
                {
                    sum += Math.Pow(data[example, feature] - averageValues[feature], 2);
                }
                standardDeviations[feature] = Math.Sqrt(sum / (numberOfExamples - 1));
            };

            return standardDeviations;
        }

        public static double[,] MeanNormalization(this double[,] data, double[] averageValues, double[] standardDeviations)
        {
            var numberOfExamples = data.GetLength(0);
            for (int example = 0; example < numberOfExamples; example++)
            {
                for (int feature = 0; feature < data.GetLength(1); feature++)
                {
                    data[example, feature] = (data[example, feature] - averageValues[feature]) / (standardDeviations[feature] != 0 ? standardDeviations[feature] : 1);
                }
            }

            return data;
        }
        
        //generate Gaussian distributed 2d array
        public static double[,] NormalDistributionSamples(int numberOfSamples, int sampleSize)
        {            
            var temp = new MultivariateNormalDistribution(dimension: sampleSize);
           
            // mean vector           
            double[] mu = temp.Generate();
            // covariance
            double[,] cov =
            {
                { 2, 1 },
                { 1, 5 }
            };

            // Create a multivariate Normal distribution
            var normal = new MultivariateNormalDistribution(mu);

            // Generate samples from it
            double[][] samples = normal.Generate(numberOfSamples);

            // Convert jagged array to 2D array
            try
            {
                int firstDim = samples.Length;
                int secondDim = samples.GroupBy(row => row.Length).Single().Key; // throws InvalidOperationException if source is not rectangular

                var result = new double[firstDim, secondDim];
                for (int i = 0; i < firstDim; ++i)
                    for (int j = 0; j < secondDim; ++j)
                        result[i, j] = samples[i][j];

                return result;
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("The given jagged array is not rectangular.");
            }
        }

        public static double[] NormalDistributionSamples(int numberOfSamples, double mean = 0, double stdDev = 1)
        {
            // Create a Normal with mean and sigma 
            var normal = new NormalDistribution(mean, stdDev);

            // Generate samples from it
            double[] samples = normal.Generate(numberOfSamples);

            return samples;
        }

    }
}
