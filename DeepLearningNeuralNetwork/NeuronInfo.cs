using System;

namespace DeepLearningNeuralNetwork
{
	public struct NeuronInfo
	{
		public double value;
		public Func<double, double> activation;
		public Func<double, double> cost;

		public NeuronInfo(double value, Func<double, double> activation, Func<double, double> cost)
		{
			this.value = value;
			this.activation = activation;
			this.cost = cost;
		}
	}
}
