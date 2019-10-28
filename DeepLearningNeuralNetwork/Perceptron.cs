using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeepLearningNeuralNetwork
{
    public class Perceptron
    {
		List<Layer> layers;

		public string Name { get; } = "Neural Netowrk";
		public int NumOfLayers { get { return layers.Count; } }
		public bool Formed { get; private set; } = false;

		public Perceptron(string name)
		{
			Name = name;
			layers = new List<Layer>();
		}

		public void Setup()
		{
			if (layers.Count < 2)
				throw new NeuralNetworkException("Не достаточно слоев для формирования связей.", layers.Count);

			if (Formed)
				throw new NeuralNetworkException("Нейронная сеть уже сформирована.");

			for (int layer = 0; layer < layers.Count - 1; layer++)
			{
				layers[layer].Bind(layers[layer + 1]);
			}

			Formed = true;
		}

		public void FeedForward(double[] inputs)
		{
			if (!Formed)
				throw new NeuralNetworkException("Нейронная сеть не сформирована.");

			layers[0].SetInputs(inputs);

			for (int layer = 1; layer < layers.Count; layer++)
			{
				layers[layer].UpdateOutputs(layers[layer - 1]);
			}
		}

		public void BackPropagation(double[] targetOutputs)
		{
			if (!Formed)
				throw new NeuralNetworkException("Нейронная сеть не сформирована.");

			layers[layers.Count - 1].UpdateGradients(targetOutputs);
			for (int layer = layers.Count - 2; layer >= 1; layer--)
			{
				layers[layer].UpdateGradients();
				layers[layer].UpdateConnections();
			}
			layers[0].UpdateConnections();
		}

		public Perceptron AddLayer(Layer layer)
		{
			layers.Add(layer);
			return this;
		}

		public Perceptron AddLayer(Layer layer, int pos)
		{
			AddLayer(layer);
			if (pos >= 0 && pos < layers.Count - 2)
				MoveLayer(layers.Count - 1, pos);
			return this;
		}

		public Perceptron MoveLayer(int from, int to)
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

			Layer temp = layers[to];
			layers[to] = layers[from];
			layers[from] = temp;

			return this;
		}

		public int GetNumOfNeurons(int layerNum, bool includeBias = true)
		{
			return includeBias ? layers[layerNum].NumOfNeurons : layers[layerNum].Length;
		}

		public double GetNeuronOutput(int layerNum, int neuronNum)
		{
			return layers[layerNum].GetNeuronOutput(neuronNum);
		}

		public double GetConnectionWeight(int layerNum, int neuronNum, int connectedNeuron)
		{
			return layers[layerNum].GetConnectionWeight(neuronNum, layers[layerNum + 1], connectedNeuron);
		}
	}
}
