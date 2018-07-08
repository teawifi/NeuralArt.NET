using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks.Helpers
{
    public static class Extensions
    {
        public static T SecondToLast<T>(this IList<T> source)
        {
            if (source.Count < 2)
                throw new ArgumentException("The list does not have at least 2 elements");

            return source[source.Count - 2];
        }
    }
}
