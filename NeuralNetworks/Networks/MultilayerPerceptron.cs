using Accord.Math;
using NeuralNetworks.Helpers;
using NeuralNetworks.Layers;
using NeuralNetworks.Optimizers;
using NeuralNetworks.PerformanceAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Optimization.CostFunction;
using NeuralNetworks.Core.Models;

namespace NeuralNetworks.Networks
{    
    public class MultilayerPerceptron : SequentialModel
    {
        List<Dense> layers;
        ICostFunction costFunction;
        IOptimizer optimizer;
        Func<double, int, List<Dense>, double> regularization;
        Func<int[], int[], double> metrics;        
        
        public MultilayerPerceptron(ICostFunction costFunction, IOptimizer optimizer, Func<double, int, List<Dense>, double> regularization, Func<int[], int[], double> metrics)
        {
            this.costFunction = costFunction;
            this.optimizer = optimizer;
            this.regularization = regularization;           
            this.metrics = metrics;
            layers = new List<Dense>();
        }   
        
        public List<Dense> Layers
        {
            get { return layers; }
            set { layers = value; }
        }     

        public override void Fit(double[,] Xtrain, int[,] Ytrain, int epochs, double learningRate, double lambda)
        {           
            var amountOfExamples = Xtrain.GetLength(0);
            int iteration = 0;
            double cost = 0;
            List<double[]> actualOutputs = new List<double[]>(Ytrain.GetLength(0));

            while (iteration < epochs)
            {
                iteration++;

                for (var i = 0; i < Xtrain.GetLength(0); i++)
                {
                    Feedforward(Xtrain.GetRow(i));
                    var output = Backpropagate(Ytrain.GetRow(i));
                    actualOutputs.Add(output);            

                    if (i % 5000 == 0)
                        Console.WriteLine("Epoch: {0} step {1}/total {2}", iteration, i, amountOfExamples);
                }

                // update weights and biases
                optimizer.Run(layers, learningRate, lambda, Xtrain.GetLength(0));
                
                Clear();

                // calculate of cost function value
                try
                {
                    if (regularization != null)
                    {
                        var term1 = costFunction.Function(Ytrain, actualOutputs);
                        var reg = regularization(lambda, amountOfExamples, layers);

                        cost = term1 + reg;
                    }                       
                    else
                        throw new InvalidOperationException("The regularization have not been defined.");
                }
                catch (Exception) { }                             

                actualOutputs.Clear();

                Console.WriteLine("Epoch: {0}, cost {1} ", iteration, cost);                
            }            
        }
        private void Feedforward(double[] x)
        {
            layers[0].Outputs = x;
            for (var i = 1; i < layers.Count; i++)
            {
                /* z = weights^(l)*a^(l-1) + bias^(l) */               
                layers[i].WeightedSum = layers[i].Weights.Dot(layers[i - 1].Outputs).Add(layers[i].Biases);

                /* a = σ(z) */
                layers[i].Outputs = layers[i].Activation.Function(layers[i].WeightedSum);                      
            }           
        }

        private double[] Backpropagate(int[] expected)
        {
            // calculate the delta of the output layer
            layers.Last().Delta = costFunction.Delta(expected, layers.Last().Outputs, layers.Last().Activation);
                       
            // calculate the sum of the last layer errors for the bias gradient
            layers.Last().ErrorsSum = layers.Last().ErrorsSum.Add(layers.Last().Delta);
            var derivativeByWeight = layers.Last().Delta.Outer(layers.SecondToLast().Outputs);

            // calculate weight gradients of the output layer
            layers.Last().WeightGradients = layers.Last().WeightGradients.Add(derivativeByWeight);

            // calculate errors and weight gradients of other layers
            for (var i = layers.Count - 2; i > 0; i--)
            {                
                var partialDerivative = layers[i].Activation.PartialDerivative(layers[i].Outputs);

                // calculate the layer error               
                layers[i].Delta = Elementwise.Multiply(
                    layers[i + 1].Weights.Transpose().Dot(layers[i + 1].Delta), partialDerivative);

                // calculate the sum of the layer errors for the bias gradient
                layers[i].ErrorsSum = layers[i].ErrorsSum.Add(layers[i].Delta);

                // calculate weight gradients of the current layer
                derivativeByWeight = layers[i].Delta.Outer(layers[i - 1].Outputs);                
                layers[i].WeightGradients = layers[i].WeightGradients.Add(derivativeByWeight);
            }            

            return (double[])layers.Last().Outputs.Clone();
        }

        public double Evaluate(double[,] x, int[] expected)
        {
            int[] predicted = new int[x.GetLength(0)];
            double[,] categories = new double[x.GetLength(0), layers.Last().Outputs.Length];
            for (var i = 0; i < x.GetLength(0); i++)
            {
                Feedforward(x.GetRow(i));
                var output = layers.Last().Outputs;
                var index = Array.FindIndex(output, item => item == output.Max());
                predicted[i] = index;                
            }           

            return metrics(predicted, expected);
        }        

        private void Clear()
        {
            for(int i = 1; i < layers.Count; i++)
            {
                Array.Clear(layers[i].ErrorsSum, 0, layers[i].ErrorsSum.Length);
                Array.Clear(layers[i].WeightedSum, 0, layers[i].WeightedSum.Length);
                Array.Clear(layers[i].WeightGradients, 0, layers[i].WeightGradients.Length);
                Array.Clear(layers[i].Outputs, 0, layers[i].Outputs.Length);
                Array.Clear(layers[i].Delta, 0, layers[i].Delta.Length);               
            }
        }    
	}
}
