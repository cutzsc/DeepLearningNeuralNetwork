using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeepLearningNeuralNetwork;


namespace Controller
{
	class Program
	{
		static void Main(string[] args)
		{
			Layer layer = new Layer()
				.AddNeuron()
				.AddNeuron(0.5)
				.AddNeuron(new Neuron(.1));

			layer.Print();

			Console.ReadLine();
		}
	}
}
