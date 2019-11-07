using DeepLearningNeuralNetwork.Neurons;

namespace DeepLearningNeuralNetwork.Layers
{
	class InputLayer : LayerFactory
	{
		public InputLayer(LayerInfo info)
			: base(info) { }

		public override Neuron[] CreateNeurons()
		{
			int len = info.hasBias ? info.neurons.Count + 1 : info.neurons.Count;
			Neuron[] neurons = new Neuron[len];
			for (int i = 0; i < info.neurons.Count; i++)
			{
				neurons[i] = new InputNeuron();
			}
			if (info.hasBias)
				neurons[len - 1] = new Bias(info.biasValue);
			return neurons;
		}
	}
}
