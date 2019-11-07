using System;
using System.Collections;
using System.Collections.Generic;

namespace DeepLearningNeuralNetwork
{
	public class Layer
	{
		public Neuron[] neurons;

		/// <summary>
		/// Create input layer.
		/// </summary>
		private Layer(int neuronsCount, double biasValue)
		{
			neurons = new Neuron[neuronsCount + 1];
			for (int i = 0; i < neuronsCount; i++)
			{
				neurons[i] = new InputNeuron();
			}
			neurons[neuronsCount] = new Bias(biasValue);
		}

		/// <summary>
		/// Create hidden layer.
		/// </summary>
		private Layer(int neuronsCount, double biasWeight, Func<double, double> activation, Func<double, double> cost)
		{
			neurons = new Neuron[neuronsCount + 1];
			for (int neuron = 0; neuron < neuronsCount; neuron++)
			{
				neurons[neuron] = new Neuron(activation, cost);
			}
			neurons[neuronsCount] = new Bias(biasWeight);
		}

		/// <summary>
		/// Create output layer.
		/// </summary>
		private Layer(int neuronsCount, Func<double, double> activation, Func<double, double> cost)
		{
			neurons = new Neuron[neuronsCount];
			for (int i = 0; i < neuronsCount; i++)
			{
				neurons[i] = new OutputNeuron(activation, cost);
			}
		}

		public static Layer CreateInputLayer(int neuronsCount, double biasValue)
		{
			return new Layer(neuronsCount, biasValue);
		}

		public static Layer CreateHiddenLayer(int neuronsCount, double biasWeight, Func<double, double> activation, Func<double, double> cost)
		{
			return new Layer(neuronsCount, biasWeight, activation, cost);
		}

		public static Layer CreateOutputLayer(int neuronsCount, Func<double, double> activation, Func<double, double> cost)
		{
			return new Layer(neuronsCount, activation, cost);
		}
	}
}
