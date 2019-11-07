using DeepLearningNeuralNetwork.Neurons;

namespace DeepLearningNeuralNetwork.Layers
{
	public class Layer
	{
		public Neuron[] neurons;

		public bool HasBias { get; }

		public Layer(LayerFactory factory)
		{
			neurons = factory.CreateNeurons();
			HasBias = factory.HasBias();
		}
	}
}
