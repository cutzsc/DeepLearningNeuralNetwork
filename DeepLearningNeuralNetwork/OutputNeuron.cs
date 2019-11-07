using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepLearningNeuralNetwork
{
	public class OutputNeuron : Neuron
	{
		public OutputNeuron(Func<double, double> activation, Func<double, double> cost)
			: base(activation, cost)
		{
			connections = new Dictionary<Neuron, Connection>(0);
		}

		public void CalculateGradient(double target)
		{
			gradient = cost(Output) * (target - Output);
		}

		public override void CalculateWeights(double eta, double alpha) { }
		public override void SetConnections(Neuron[] neurons, double min, double max) { }
	}
}
