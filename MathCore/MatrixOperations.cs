using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCore
{
    public static class MatrixOperations
    {
        public static double[] Sum(this double[,] matrix, int dimension)
        {
            if (matrix == null)
                throw new ArgumentNullException("matrix");

            double[] results = { };

            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);

            if (dimension == 0)
            {
                results = new double[columns];

                for (int j = 0; j < columns; j++)
                {
                    double sum = 0;
                    for (int i = 0; i < rows; i++)
                    {
                        sum += matrix[i, j];
                    }

                    results[j] = sum;
                }
            }
            else if (dimension == 1)
            {
                results = new double[rows];

                for (int i = 0; i < rows; i++)
                {
                    double sum = 0;
                    for (int j = 0; j < columns; j++)
                    {
                        sum += matrix[i, j];
                    }

                    results[i] = sum;
                }
            }

            return results;
        }

        public static double[] GetRow(this double[,] matrix, int rowId)
        {
            int dim1 = matrix.GetLength(0);
            int dim2 = matrix.GetLength(1);
            const int doubleSize = sizeof(double);

            double[] row = new double[dim2];

            Buffer.BlockCopy(matrix, doubleSize * dim2 * rowId, row, 0, doubleSize * dim2);

            return row;
        }

        public static int[] GetRow(this int[,] matrix, int rowId)
        {
            int dim1 = matrix.GetLength(0);
            int dim2 = matrix.GetLength(1);
            const int size = sizeof(int);

            int[] row = new int[dim2];

            Buffer.BlockCopy(matrix, size * dim2 * rowId, row, 0, size * dim2);

            return row;
        }
    }
}
