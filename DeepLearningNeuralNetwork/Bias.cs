using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepLearningNeuralNetwork
{
	public class Bias : Neuron
	{
		public Bias(double weight)
			: base(weight, null, null) { }

		public override void CalculateOutput(Neuron[] neurons) { }
		public override void CalculateGradient() { }
	}
}
