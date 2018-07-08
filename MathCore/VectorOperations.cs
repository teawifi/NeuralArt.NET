using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCore
{
    public static class VectorOperations
    {
        public static double Sum(this double[] vector)
        {
            if (vector == null)
                throw new ArgumentNullException("vector");

            double sum = 0;
            for (int i = 0; i < vector.Count(); i++)
            {
                sum += vector[i];
            }

            return sum;
        }

        /// a logarithm to the base e
        public static double[] LogExp(this double[] array)
        {
            double[] result = new double[array.Length];
            const double C = 0.0000000001;

            for (int i = 0; i < array.Length; i++)
            {
                result[i] = Math.Log(array[i] + C, Math.E);
            }

            return result;
        }       
    }
}
