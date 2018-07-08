using MathCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatasetPreprocessing
{
    public static class DataHelper
    {
        public static int[,] LabelsToCategories(int[] labels, int numberOfCategories)
        {
            int[,] categories = new int[labels.Length, numberOfCategories];

            for (int i = 0; i < labels.Length; i++)
                categories[i, labels[i]] = 1;

            return categories;
        }

        public static int[] CategoriesToLabels(double[,] categories, int numberOfLabels)
        {
            int[] labels = new int[categories.GetLength(0)];

            for (int i = 0; i < categories.GetLength(0); i++)
            {
                double[] rowVector = categories.GetRow(i);
                var ind = Array.FindIndex(rowVector, item => item == rowVector.Max());
                labels[i] = ind;
            }

            return labels;
        }

        public static double[,] Scale(this double[,] matrix, int denominator)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    matrix[i, j] /= denominator;

            return matrix;
        }

        public static T[,] To2D<T>(this T[][] source)
        {
            try
            {
                int firstDim = source.Length;
                int secondDim = source.GroupBy(row => row.Length).Single().Key; // throws InvalidOperationException if source is not rectangular

                var result = new T[firstDim, secondDim];
                for (int i = 0; i < firstDim; ++i)
                    for (int j = 0; j < secondDim; ++j)
                        result[i, j] = source[i][j];

                return result;
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("The given jagged array is not rectangular.");
            }
        }
    }
}
