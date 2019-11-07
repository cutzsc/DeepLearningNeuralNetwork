using System;
using System.Collections.Generic;

namespace DeepLearningNeuralNetwork
{
	public class Neuron
	{
		protected double gradient;
		protected Dictionary<Neuron, Connection> connections;
		protected Func<double, double> activation;
		protected Func<double, double> cost;

		public double Output { get; protected set; }

		public Neuron(Func<double, double> activation, Func<double, double> cost)
			: this(0, activation, cost) { }

		public Neuron(double weight, Func<double, double> activation, Func<double, double> cost)
		{
			Output = weight;
			this.activation = activation;
			this.cost = cost;
		}

		public virtual void SetConnections(Neuron[] neurons, double minWeight, double maxWeight)
		{
			connections = new Dictionary<Neuron, Connection>(neurons.Length);
			for (int i = 0; i < neurons.Length; i++)
			{
				connections.Add(neurons[i], new Connection(Functions.NextDouble(minWeight, maxWeight)));
			}
		}

		public virtual void CalculateOutput(Neuron[] neurons)
		{
			double sum = 0;
			for (int i = 0; i < neurons.Length; i++)
			{
				sum += neurons[i].Output * neurons[i].connections[this].weight;
			}
			Output = activation(sum);
		}

		public virtual void CalculateGradient()
		{
			double sum = 0;
			foreach (Neuron n in connections.Keys)
			{
				sum += n.gradient * connections[n].weight;
			}
			gradient = cost(Output) * sum;
		}

		public virtual void CalculateWeights(double eta, double alpha)
		{
			foreach (Neuron n in connections.Keys)
			{
				Connection c = connections[n];
				c.deltaWeight = eta * Output * n.gradient +
					alpha * c.deltaWeight;
				c.weight += c.deltaWeight;
			}
		}
	}
}
