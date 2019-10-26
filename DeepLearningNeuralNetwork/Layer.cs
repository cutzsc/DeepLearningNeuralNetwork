using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepLearningNeuralNetwork
{
	public class Layer
	{
		private List<Neuron> neurons;

		public bool Confirmed { get; private set; }

		/// <summary>
		/// Инициализировать слой.
		/// </summary>
		/// <param name="count"></param>
		public Layer(int count = 0)
		{
			Confirmed = false;
			neurons = new List<Neuron>();

			for (int i = 0; i < count; i++)
			{
				AddNeuron();
			}
		}

		public void Confirm()
		{
			Confirmed = true;
		}

		/// <summary>
		/// Добавляет нейрон в слой.
		/// </summary>
		/// <param name="n"></param>
		/// <returns></returns>
		public Layer AddNeuron(Neuron n)
		{
			if (Confirmed)
				throw new Exception("Слой уже завершен.");
			neurons.Add(n);
			return this;
		}

		/// <summary>
		/// Добавить нейрон с весом [weight].
		/// </summary>
		public Layer AddNeuron(double weight)
		{
			AddNeuron(new Neuron(weight));
			return this;
		}

		/// <summary>
		/// Добавить нейрон со случайными весами.
		/// </summary>
		public Layer AddNeuron(double min = 0, double max = 1)
		{
			Random rand = new Random();
			AddNeuron(new Neuron(rand.NextDouble() * (max - min) - min));
			return this;
		}

		public void Print()
		{
			for (int neuron = 0; neuron < neurons.Count; neuron++)
			{
				Console.Write(neurons[neuron].Weight.ToString("0.000"));
				if (neuron < neurons.Count - 1)
					Console.Write("\t");
			}
		}
	}
}
