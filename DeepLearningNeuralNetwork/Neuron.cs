using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepLearningNeuralNetwork
{
	public class Neuron
	{
		public double Weight { get; private set; }

		public Neuron(double weight)
		{
			Weight = weight;
		}
	}
}
