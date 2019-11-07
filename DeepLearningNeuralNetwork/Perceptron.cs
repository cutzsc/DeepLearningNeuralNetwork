using DeepLearningNeuralNetwork.Layers;
using DeepLearningNeuralNetwork.Neurons;
using System;

namespace DeepLearningNeuralNetwork
{
	public class Perceptron
    {
		Layer[] layers;

		public double[] Result
		{
			get
			{
				double[] result = new double[layers[layers.Length - 1].neurons.Length];
				int i = 0;
				foreach (Neuron n in layers[layers.Length - 1].neurons)
					result[i++] = n.Output;
				return result;
			}
		}

		public Perceptron(PerceptronInfo info)
		{
			// Create perceptron
			layers = new Layer[info.layers.Count];

			layers[0] = new Layer(new InputLayer(info.layers[0]));

			for (int i = 1; i < layers.Length - 1; i++)
				layers[i] = new Layer(new HiddenLayer(info.layers[i]));

			layers[layers.Length - 1] = new Layer(new OutputLayer(info.layers[info.layers.Count - 1]));

			// Create connections
			for (int i = 0; i < layers.Length - 1; i++)
				for (int j = 0; j < layers[i].neurons.Length; j++)
					layers[i].neurons[j].SetConnections(layers[i + 1].neurons, info.minWeight, info.maxWeight);
		}

		public void FeedForward(double[] inputs)
		{
			// Check length == length

			Neuron[] inputLayer = layers[0].neurons;
			for (int i = 0; i < inputLayer.Length; i++)
			{
				InputNeuron n = inputLayer[i] as InputNeuron;
				if (n != null)
					n.SetInput(inputs[i]);
			}

			for (int i = 1; i < layers.Length; i++)
				foreach (Neuron n in layers[i].neurons)
				{
					if (n is Bias)
						continue;
					n.CalculateOutput(layers[i - 1].neurons);
				}

		}

		public void BackPropagation(double[] targets, double eta, double alpha)
		{
			// Check length == length

			int i = 0;

			foreach (OutputNeuron neuron in layers[layers.Length - 1].neurons)
				neuron.CalculateGradient(targets[i++]);

			for (i = layers.Length - 2; i >= 0; i--)
				foreach (Neuron n in layers[i].neurons)
				{
					n.CalculateGradient();
					n.CalculateWeights(eta, alpha);
				}
		}
	}
}
