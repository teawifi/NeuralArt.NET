using NeuralNetworks.Helpers;
using NeuralNetworks.Layers;
using NeuralNetworks.Networks;
using NeuralNetworks.Optimizers;
using NeuralNetworks.PerformanceAnalysis;
using System;
using Accord.DataSets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Math;
using System.IO;
using System.Globalization;
using DatasetPreprocessing;
using MathCore;
using MathCore.ActivationFunctions;
using Optimization.CostFunction;
using Optimization;

namespace MNISTHandwritingRecognition
{
    class Program
    {
        static void Main(string[] args)
        {
            string pathToDir = @".....Datasets\MNIST\";
            
            // Get the training set
            DatasetSplit dataset = CsvParser.Read(pathToDir + "mnist_train.csv");
            double[,] xTrain = dataset.X;
            int[] yTrain = dataset.Y;

            // Get the testing set
            dataset = CsvParser.Read(pathToDir + "mnist_test.csv");
            double[,] xTest = dataset.X;
            int[] yTest = dataset.Y;

            // scale data
            xTrain = xTrain.Scale(denominator: 255);
            xTest = xTest.Scale(denominator: 255);            

            // convert output labels to one-hot vectors
            int[,] yTrainCategorial = DataHelper.LabelsToCategories(yTrain, numberOfCategories: 10);            

            MultilayerPerceptron mlp = new MultilayerPerceptron(
                costFunction: new CrossEntropy(),
                optimizer: new GradientDescent(),
                regularization: new Regularization().L2,
                metrics: new MulticlassClassificationMetrics().Accuracy);

            // 784 14 15 10
            mlp.Layers.Add(
                new Dense(numberOfInputs: 0, numberOfOutputs: 784, activation: new Sigmoid()));
            mlp.Layers.Add(
                new Dense(numberOfInputs: 784, numberOfOutputs: 30, activation: new Sigmoid()));           
            mlp.Layers.Add(
                new Dense(numberOfInputs: 30, numberOfOutputs: 10, activation: new Sigmoid()));

            mlp.Fit(xTrain, yTrainCategorial, epochs: 400, learningRate: 0.01, lambda: 0.1);

            double accuracyTrain = mlp.Evaluate(xTrain, yTrain);
            Console.WriteLine("Accuracy of train data: {0}", accuracyTrain);

            double accuracy = mlp.Evaluate(xTest, yTest);
            Console.WriteLine("Accuracy of test data: {0}", accuracy);            

            Console.ReadKey();
        }        
    }
}
