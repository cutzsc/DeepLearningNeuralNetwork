using System.Collections.Generic;

namespace DeepLearningNeuralNetwork
{
	public struct PerceptronInfo
	{
		public List<LayerInfo> layers;
		public double minWeight;
		public double maxWeight;

		public static PerceptronInfo CreateClassicPerceptron(double minWeight, double maxWeight, params int[] net)
		{
			PerceptronInfo perceptron = new PerceptronInfo(minWeight, maxWeight);
			for (int l = 0; l < net.Length; l++)
			{
				LayerInfo layer = l < net.Length - 1 ?
					LayerInfo.CreateLayerWithBias :
					LayerInfo.CreateDefaultLayer;
				for (int n = 0; n < net[l]; n++)
					layer.AddNeuron(NeuronInfo.CreateDefaultNeuron);
				perceptron.AddLayer(layer);
			}
			return perceptron;
		}

		public PerceptronInfo(double minWeight, double maxWeight, params LayerInfo[] layers)
		{
			this.minWeight = minWeight;
			this.maxWeight = maxWeight;
			this.layers = new List<LayerInfo>();
			foreach (LayerInfo layer in layers)
			{
				this.layers.Add(layer);
			}
		}

		public PerceptronInfo AddLayer(LayerInfo layer)
		{
			layers.Add(layer);
			return this;
		}

		public PerceptronInfo AddLayer(LayerInfo neuron, int pos)
		{
			layers.Add(neuron);
			if (pos >= 0 && pos < layers.Count - 2)
				MoveLayer(pos, layers.Count - 1);
			return this;
		}

		public PerceptronInfo MoveLayer(int from, int to)
		{
			if (from == to)
				return this;

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

			return this;
		}
	}
}
