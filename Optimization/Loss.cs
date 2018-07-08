using Accord.Math;
using MathCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimization
{
    public static class Loss
    {
        public static double[] SquaredLoss(this double[] errors)
        {
            for (int i = 0; i < errors.Length; i++)
                errors[i] *= errors[i];

            return errors;
        }

        public static double[] SquaredLoss(double[] desired, double[] actual)
        {
            double[] errors = actual.Subtract(desired);
            return errors.SquaredLoss();
        }

        public static double[] SquaredLoss(int[] desired, double[] actual)
        {
            double[] errors = actual.Subtract(desired);
            return errors.SquaredLoss();
        }

        public static double[] Entropy(int[] desired, double[] actual)
        {
            return Elementwise.Multiply((-1).Multiply(desired), actual.LogExp())
                .Subtract(Elementwise.Multiply(1.Subtract(desired), 1.Subtract(actual).LogExp()));
        }
    }
}
