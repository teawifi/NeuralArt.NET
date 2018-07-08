using System;
using Accord.Statistics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathCore;
using MathCore.ActivationFunctions;
using NeuralNetworks.Core.Layers;

namespace NeuralNetworks.Layers
{
    public class Dense : ILayer
    {
        private int numberOfInputs;
        private int numberOfOutputs;
        private double[] weightedSum;
        private double[] outputs;
        private double[,] weights;
        private double[,] weightGradients;
        private double[] biases;
        private double[] delta; //error of the layer
        private double[] errorsSum; // bias gradient

        private IActivationFunction activationFunction;

        Random random = new Random();

        public int NumberOfInputs { get { return numberOfInputs; } }
        public int NumberOfOutputs { get { return numberOfOutputs; } }       

        public double[] WeightedSum { get { return weightedSum; } set { weightedSum = value; } } //weighted sum of this layer (z)
        public double[] Outputs { get { return outputs; } set { outputs = value; } } //outputs of this layer        
        public double[,] Weights { get { return weights; } set { weights = value; } }
        public double[] Biases { get { return biases; } set { biases = value; } }
        public double[] Delta { get { return delta; } set { delta = value; } }
        public double[,] WeightGradients { get { return weightGradients; } set { weightGradients = value; } } //deltas of this layer
        public double[] ErrorsSum { get { return errorsSum; } set { errorsSum = value; } }
                

        public IActivationFunction Activation
        {
            get { return activationFunction; }
            set { activationFunction = value; }
        }

        public Dense(int numberOfInputs, int numberOfOutputs, IActivationFunction activation = null)
        {
            this.numberOfInputs = numberOfInputs;
            this.numberOfOutputs = numberOfOutputs;

            // 
            if (numberOfInputs != 0) 
            {
                weightedSum = new double[numberOfOutputs];
                weights = new double[numberOfOutputs, numberOfInputs];
                weightGradients = new double[numberOfOutputs, numberOfInputs];
                biases = new double[numberOfOutputs];
                delta = new double[numberOfOutputs];
                errorsSum = new double[numberOfOutputs];
                this.activationFunction = activation;
                Init();
            }
           
            outputs = new double[numberOfOutputs];            
        }

        private void Init()
        {
            //Accord.Math.Random.Generator.Seed = 0;

            //weights = Statistics.NormalDistributionSamples(numberOfSamples: weights.GetLength(0), sampleSize: weights.GetLength(1));
            //biases = Statistics.NormalDistributionSamples(numberOfSamples: biases.Length, stdDev: 1/Math.Sqrt(1000));


            for (int i = 0; i < weights.GetLength(0); i++)
                for (int j = 0; j < weights.GetLength(1); j++)
                    weights[i, j] = random.NextDouble() * 2 - 1;

            for (int i = 0; i < biases.Length; i++)
                biases[i] = random.NextDouble() * 2 - 1;
        }
    }
}
