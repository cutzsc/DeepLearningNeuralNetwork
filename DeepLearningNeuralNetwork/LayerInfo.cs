using System.Collections.Generic;

namespace DeepLearningNeuralNetwork
{
	public struct LayerInfo
	{
		public List<NeuronInfo> neurons;
		public bool hasBias;

		public LayerInfo(bool hasBias, params NeuronInfo[] neurons)
		{
			this.hasBias = hasBias;
			this.neurons = new List<NeuronInfo>();
			foreach (NeuronInfo neuron in neurons)
			{
				this.neurons.Add(neuron);
			}
		}

		public void AddNeuron(NeuronInfo neuron, int pos)
		{
			neurons.Add(neuron);
			if (pos >= 0 && pos < neurons.Count - 2)
				MoveNeuron(pos, neurons.Count - 1);
		}

		public void MoveNeuron(int from, int to)
		{
			if (from == to)
				return;

			if (to < 0)
				to = 0;
			else if (to > neurons.Count - 1)
				to = neurons.Count - 1;

			if (from < 0)
				from = 0;
			else if (from > neurons.Count - 1)
				from = neurons.Count - 1;

			NeuronInfo temp = neurons[to];
			neurons[to] = neurons[from];
			neurons[from] = temp;
		}
	}
}
