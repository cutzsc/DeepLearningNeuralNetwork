using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepLearningNeuralNetwork.Neurons
{
	public class InputNeuron : Neuron
	{
		public InputNeuron()
			: base(null, null) { }

		public void SetInput(double input)
		{
			Output = input;
		}

		public override void CalculateOutput(Neuron[] neurons) { }
		public override void CalculateGradient() { }
	}
}
