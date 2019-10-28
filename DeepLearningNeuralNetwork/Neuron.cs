using System;
using System.Collections.Generic;

namespace DeepLearningNeuralNetwork
{
	public class Neuron
	{
		Dictionary<Neuron, Connection> connections;

		double eta = 0.35;
		double alpha = 0.95;
		double gradient;
		
		public double Output { get; private set; }

		public Neuron(double weight = 0)
		{
			Output = weight;
		}

		public void SetConnections(List<Neuron> neurons)
		{
			connections = new Dictionary<Neuron, Connection>(neurons.Count);
			foreach (Neuron n in neurons)
			{
				double m = 0.15;
				connections.Add(n, new Connection(Functions.Rand.NextDouble() * (m - -m) - m));
			}
		}

		public void SetOutput(double value)
		{
			Output = value;
		}

		public void CalculateOutput(List<Neuron> neurons)
		{
			double sum = 0;
			foreach (Neuron n in neurons)
			{
				sum += n.Output * n.connections[this].weight;
			}
			Output = Functions.Sigmoid(sum);
		}

		public void CalculateGradient(double targetOutput)
		{
			gradient = Functions.SigmoidDerivative(Output) * (targetOutput - Output);
		}

		public void CalculateGradient()
		{
			double sum = 0;
			foreach (Neuron n in connections.Keys)
			{
				sum += n.gradient * connections[n].weight;
			}
			gradient = Functions.SigmoidDerivative(Output) * sum;
		}

		public void CalculateConnections()
		{
			foreach (Neuron n in connections.Keys)
			{
				Connection c = connections[n];
				c.deltaWeight = eta * Output * n.gradient +
					alpha * c.deltaWeight;
				c.weight += c.deltaWeight;
			}
		}

		public double GetConnectionWeight(Neuron n)
		{
			return connections[n].weight;
		}
	}
}
