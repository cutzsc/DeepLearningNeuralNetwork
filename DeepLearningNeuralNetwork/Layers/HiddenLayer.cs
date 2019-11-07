using System;
using DeepLearningNeuralNetwork.Neurons;

namespace DeepLearningNeuralNetwork.Layers
{
	class HiddenLayer : LayerFactory
	{
		public HiddenLayer(LayerInfo info)
			: base(info) { }

		public override Neuron[] CreateNeurons()
		{
			int len = info.hasBias ? info.neurons.Count + 1 : info.neurons.Count;
			Neuron[] neurons = new Neuron[len];
			for (int i = 0; i < info.neurons.Count; i++)
			{
				NeuronInfo n = info.neurons[i];
				neurons[i] = new Neuron(n.activation, n.cost);
			}
			if (info.hasBias)
				neurons[len - 1] = new Bias(info.biasValue);
			return neurons;
		}
	}
}
