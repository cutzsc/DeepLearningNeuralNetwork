using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepLearningNeuralNetwork
{
	public class NeuralNetworkException : Exception
	{
		public int Got { get; }
		public int Expected { get; }
		public int LayersCount { get; }

		public NeuralNetworkException(string message)
			: base(message) { }

		public NeuralNetworkException(string message, int got, int expected)
			: base(message)
		{
			Got = got;
			Expected = expected;
		}

		public NeuralNetworkException(string message, int layersCount)
			: base(message)
		{
			LayersCount = layersCount;
		}
	}
}
