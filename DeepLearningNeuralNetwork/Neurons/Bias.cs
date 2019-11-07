using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepLearningNeuralNetwork.Neurons
{
	public class Bias : Neuron
	{
		public Bias(double value)
			: base(null, null)
		{
			Output = value;
		}

		public override void CalculateOutput(Neuron[] neurons) { }
		public override void CalculateGradient() { }
	}
}
