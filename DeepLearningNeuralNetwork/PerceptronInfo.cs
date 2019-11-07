using System.Collections.Generic;

namespace DeepLearningNeuralNetwork
{
	public struct PerceptronInfo
	{
		public List<LayerInfo> layers;

		public void AddLayer(LayerInfo neuron, int pos)
		{
			layers.Add(neuron);
			if (pos >= 0 && pos < layers.Count - 2)
				MoveLayer(pos, layers.Count - 1);
		}

		public void MoveLayer(int from, int to)
		{
			if (from == to)
				return;

			if (to < 0)
				to = 0;
			else if (to > layers.Count - 1)
				to = layers.Count - 1;

			if (from < 0)
				from = 0;
			else if (from > layers.Count - 1)
				from = layers.Count - 1;

			LayerInfo temp = layers[to];
			layers[to] = layers[from];
			layers[from] = temp;
		}
	}
}
