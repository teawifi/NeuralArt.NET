using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatasetPreprocessing
{
    public class DatasetSplit
    {
        public double[,] X { get; set; }
        public int[] Y { get; set; }

        //public DatasetSplit SplitIntoInputAndOutputSets(double[,] set, int outputColumn)
        //{
        //    double[,] xSet = new double[set.Count(), set.ElementAt(0).Count() - 1];
        //    int[] ySet = new int[set.Count()];

        //    // select an input set
        //    if (outputColumn == 0)
        //    {
        //        for (int i = 0; i < set.Count(); i++)
        //        {
        //            for (int j = 1; j < set.ElementAt(0).Count(); j++)
        //            {
        //                var item = set.ElementAt(i)[j];
        //                var num = Double.Parse(item, CultureInfo.InvariantCulture);
        //                xSet[i, j - 1] = num;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        for (int i = 0; i < set.Count(); i++)
        //        {
        //            for (int j = 0; j < set.ElementAt(0).Count() - 1; j++)
        //            {
        //                var item = set.ElementAt(i)[j];
        //                var num = Double.Parse(item, CultureInfo.InvariantCulture);
        //                xSet[i, j] = num;
        //            }
        //        }
        //    }

        //    // select an output set
        //    for (int i = 0; i < set.Count(); i++)
        //    {
        //        var item = set.ElementAt(i)[outputColumn];
        //        var num = Int32.Parse(item, CultureInfo.InvariantCulture);
        //        ySet[i] = num;
        //    }

        //    return new DatasetSplit() { X = xSet, Y = ySet };
        //}
    }
}
