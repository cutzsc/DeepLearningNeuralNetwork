using System;

namespace DeepLearningNeuralNetwork
{
	public static class Functions
	{
		public static Random Rand { get; }

		static Functions()
		{
			Rand = new Random();
		}

		public static double Sigmoid(double x)
		{
			return 1 / (1 + Math.Exp(-x));
		}

		public static double SigmoidDerivative(double x)
		{
			return x * (1 - x);
		}
	}
}
