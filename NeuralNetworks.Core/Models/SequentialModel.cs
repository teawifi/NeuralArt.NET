using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks.Core.Models
{
    public class SequentialModel : IModel
    {
        public virtual void Fit(double[,] Xtrain, int[,] Ytrain, int epochs, double learningRate, double lambda)
        {
            throw new NotImplementedException();
        }
    }
}
