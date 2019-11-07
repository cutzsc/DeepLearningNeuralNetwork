using DeepLearningNeuralNetwork.Neurons;

namespace DeepLearningNeuralNetwork.Layers
{
	public class Layer
	{
		public Neuron[] neurons;
		public Layer(LayerFactory factory)
		{
			neurons = factory.CreateNeurons();
		}
	}
}
