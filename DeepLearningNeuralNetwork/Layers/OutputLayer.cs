using DeepLearningNeuralNetwork.Neurons;
using System.Linq;

namespace DeepLearningNeuralNetwork.Layers
{
	class OutputLayer : LayerFactory
	{
		public OutputLayer(LayerInfo info)
			: base(info) { }

		public override Neuron[] CreateNeurons()
		{
			int len = info.neurons.Count;
			Neuron[] neurons = new Neuron[len];
			for (int i = 0; i < len; i++)
			{
				NeuronInfo n = info.neurons[i];
				neurons[i] = new OutputNeuron(n.activation, n.cost);
			}
			return neurons;
		}
	}
}
