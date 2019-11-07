using DeepLearningNeuralNetwork.Layers;
using DeepLearningNeuralNetwork.Neurons;
using System;
using System.Threading.Tasks;

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
			int len = layers[0].HasBias ?
				layers[0].neurons.Length - 1 :
				layers[0].neurons.Length;

			if (inputs.Length != len)
				throw new NeuralNetworkException("", inputs.Length, len);

			Neuron[] inputNeurons = layers[0].neurons;
			for (int i = 0; i < len; i++)
			{
				InputNeuron n = inputNeurons[i] as InputNeuron;
				n.SetInput(inputs[i]);
			}

			for (int i = 1; i < layers.Length; i++)
			{
				foreach (Neuron neuron in layers[i].neurons)
				{
					neuron.CalculateOutput(layers[i - 1].neurons);
				}
			}
		}

		public void ParallelFeedForward(double[] inputs)
		{
			int len = layers[0].HasBias ?
				layers[0].neurons.Length - 1 :
				layers[0].neurons.Length;

			if (inputs.Length != len)
				throw new NeuralNetworkException("", inputs.Length, len);

			Neuron[] inputNeurons = layers[0].neurons;
			for (int i = 0; i < len; i++)
			{
				InputNeuron n = inputNeurons[i] as InputNeuron;
				Task.Run(() => n.SetInput(inputs[i]));
			}

			Task.WaitAll();

			for (int i = 1; i < layers.Length; i++)
			{
				foreach (Neuron n in layers[i].neurons)
				{
					Task.Run(() => n.CalculateOutput(layers[i - 1].neurons));
				}
				Task.WaitAll();
			}
		}

		public void BackPropagation(double[] targets, double eta, double alpha)
		{
			if (targets.Length != layers[layers.Length - 1].neurons.Length)
				throw new NeuralNetworkException("", targets.Length, layers[layers.Length - 1].neurons.Length);

			int i = 0;
			foreach (OutputNeuron neuron in layers[layers.Length - 1].neurons)
			{
				neuron.CalculateGradient(targets[i++]);
			}

			for (i = layers.Length - 2; i >= 0; i--)
			{
				foreach (Neuron neuron in layers[i].neurons)
				{
					neuron.CalculateGradient();
					neuron.CalculateWeights(eta, alpha);
				}
			}
		}

		public void ParallelBackPropagation(double[] targets, double eta, double alpha)
		{
			if (targets.Length != layers[layers.Length - 1].neurons.Length)
				throw new NeuralNetworkException("", targets.Length, layers[layers.Length - 1].neurons.Length);

			int i = 0;
			foreach (OutputNeuron neuron in layers[layers.Length - 1].neurons)
			{
				Task.Run(() => neuron.CalculateGradient(targets[i++]));
			}

			Task.WaitAll();

			for (i = layers.Length - 2; i >= 0; i--)
			{
				foreach (Neuron neuron in layers[i].neurons)
				{
					Task.Run(() =>
					{
						neuron.CalculateGradient();
						neuron.CalculateWeights(eta, alpha);
					});
				}
				Task.WaitAll();
			}
		}
	}
}
