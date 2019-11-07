using DeepLearningNeuralNetwork.Neurons;

namespace DeepLearningNeuralNetwork.Layers
{
	public abstract class LayerFactory
	{
		protected LayerInfo info;
		public LayerFactory(LayerInfo info)
		{
			this.info = info;
		}
		public abstract Neuron[] CreateNeurons();

		public bool HasBias()
		{
			return info.hasBias;
		}
	}
}
