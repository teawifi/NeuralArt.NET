using MathCore.ActivationFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimization.CostFunction
{
    public interface ICostFunction
    {
        double Function(int[,] desired, List<double[]> actual);

        double[] Delta(int[] desired, double[] actual, IActivationFunction activation);
    }
}
