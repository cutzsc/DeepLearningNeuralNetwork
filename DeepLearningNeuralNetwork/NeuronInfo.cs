using System;

namespace DeepLearningNeuralNetwork
{
	public struct NeuronInfo
	{
		public Func<double, double> activation;
		public Func<double, double> cost;

		/// <summary>
		/// Neuron with sigmoid functions.
		/// </summary>
		public static NeuronInfo CreateDefaultNeuron { get { return new NeuronInfo(Functions.Sigmoid, Functions.SigmoidDerivative); } }

		public NeuronInfo(Func<double, double> activation, Func<double, double> cost)
		{
			this.activation = activation;
			this.cost = cost;
		}
	}
}
