using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepLearningNeuralNetwork
{
	struct Connection
	{
		public double weight;
		public Neuron pre;
		public Neuron next;
	}
}
