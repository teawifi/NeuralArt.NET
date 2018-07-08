using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCore.ActivationFunctions
{
    public interface IActivationFunction
    {
        double[] Function(double[] z);
        double[] PartialDerivative(double[] activatedOutputs);
    }
}
