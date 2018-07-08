using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatasetPreprocessing
{
    public static class CsvParser
    {
        public static DatasetSplit Read(string path)
        {
            List<double[]> xList = new List<double[]>();
            List<int> yList = new List<int>();

            using (TextFieldParser parser = new TextFieldParser(path))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
               
                while (!parser.EndOfData)                
                {
                    //Process row
                    string[] fields = parser.ReadFields();

                    double[] array = new double[fields.Length-1];

                    for (int i = 1; i < fields.Count(); i++)
                        array[i-1] = Double.Parse(fields[i], CultureInfo.InvariantCulture);

                    xList.Add(array);
                                        
                    var number = Int32.Parse(fields[0], CultureInfo.InvariantCulture);
                    yList.Add(number);                    
                }
            }            

            double[,] xArray = new double[xList.Count, xList[0].Length];

            for (int i = 0; i < xList.Count; i++)
            {
                for (int j = 0; j < xList[0].Length; j++)
                {
                    xArray[i, j] = xList[i][j];
                }
            }

            var dataset = new DatasetSplit();
            dataset.X = xArray;
            dataset.Y = yList.ToArray();

            return dataset;
        }        
    }
}
