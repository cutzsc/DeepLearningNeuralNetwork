using System.Collections;
using System.Collections.Generic;

namespace DeepLearningNeuralNetwork
{
	public class Layer
	{
		List<Neuron> neurons;

		public bool HasBias { get; private set; }
		public int NumOfNeurons { get { return neurons.Count; } }
		public int Length => HasBias ? neurons.Count - 1 : neurons.Count;

		public Layer(int numNeurons = 0, bool useBias = true)
		{
			neurons = new List<Neuron>();
			for (int i = 0; i < numNeurons; i++)
			{
				neurons.Add(new Neuron());
			}
			ToggleBias(useBias);
		}

		public Neuron ToggleBias(bool toggle)
		{
			Neuron neuron = null;
			if (toggle)
			{
				neuron = new Neuron(1);
				neurons.Add(neuron);
			}
			else if (HasBias)
			{
				neuron = neurons[neurons.Count - 1];
				neurons.RemoveAt(neurons.Count - 1);
			}
			HasBias = toggle;
			return neuron;
		}

		public void Bind(Layer nextLayer)
		{
			foreach (Neuron n in neurons)
				n.SetConnections(nextLayer.neurons);

			neurons.Capacity = neurons.Count;
		}

		public void SetInputs(double[] inputs)
		{
			if (inputs.Length != Length)
				throw new NeuralNetworkException("Количество входных значений не равно количеству нейронов во входном слое.",
					inputs.Length, neurons.Count);

			for (int neuron = 0; neuron < Length; neuron++)
			{
				neurons[neuron].SetOutput(inputs[neuron]);
			}
		}

		public void UpdateOutputs(Layer prevLayer)
		{
			for (int neuron = 0; neuron < Length; neuron++)
			{
				neurons[neuron].CalculateOutput(prevLayer.neurons);
			}
		}

		public void UpdateGradients(double[] targetOutputs)
		{
			if (targetOutputs.Length != Length)
				throw new NeuralNetworkException("Количество ожидаемых значений не равно количеству нейронов в выходном слое.",
					targetOutputs.Length, Length);

			for (int neuron = 0; neuron < Length; neuron++)
			{
				neurons[neuron].CalculateGradient(targetOutputs[neuron]);
			}
		}

		public void UpdateGradients()
		{
			for (int neuron = 0; neuron < Length; neuron++)
			{
				neurons[neuron].CalculateGradient();
			}
		}

		public void UpdateConnections()
		{
			foreach (Neuron n in neurons)
			{
				n.CalculateConnections();
			}
		}

		public Layer AddNeuron(Neuron neuron)
		{
			neurons.Add(neuron);
			return this;
		}

		public Layer AddNeuron(Neuron neuron, int pos)
		{
			AddNeuron(neuron);
			if (pos >= 0 && pos < neurons.Count - 2)
				MoveNeuron(pos, neurons.Count - 1);
			return this;
		}

		public Layer MoveNeuron(int from, int to)
		{
			if (from == to)
				return this;

			if (to < 0)
				to = 0;
			else if (to > neurons.Count - 1)
				to = neurons.Count - 1;

			if (from < 0)
				from = 0;
			else if (from > neurons.Count - 1)
				from = neurons.Count - 1;

			Neuron temp = neurons[to];
			neurons[to] = neurons[from];
			neurons[from] = temp;

			return this;
		}

		public double GetNeuronOutput(int neuronNum)
		{
			return neurons[neuronNum].Output;
		}

		public double GetConnectionWeight(int neuronNum, Layer nextLayer, int connectedNeuron)
		{
			return neurons[neuronNum].GetConnectionWeight(nextLayer.neurons[connectedNeuron]);
		}
	}
}
