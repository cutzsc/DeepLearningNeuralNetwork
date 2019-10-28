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
			Perceptron p = new Perceptron("Deep Learning");

			Layer inputLayer = new Layer(2);
			Layer l2 = new Layer(3);
			Layer l3 = new Layer(3);
			Layer l4 = new Layer(4);
			Layer outputLayer = new Layer(1, false);

			p.AddLayer(inputLayer)
				.AddLayer(l2)
				.AddLayer(l3)
				//.AddLayer(l4)
				.AddLayer(outputLayer);

			p.Setup();

			FullInfo(p);

			Train(p, 50000);

			FullInfo(p);

			Console.ReadLine();
		}

		static double[][][] data = new double[4][][]
		{
			new double[2][]
			{
				new double[2] { 0, 0 },
				new double[1] { 1 }
			},
			new double[2][]
			{
				new double[2] { 0, 1 },
				new double[1] { 0 }
			},
			new double[2][]
			{
				new double[2] { 1, 0 },
				new double[1] { 0 }
			},
			new double[2][]
			{
				new double[2] { 1, 1 },
				new double[1] { 1 }
			}
		};

		static double[][] TrainingData()
		{
			int next = Functions.Rand.Next(0, 4);

			return data[next];
		}

		static void Train(Perceptron p, int times)
		{
			for (int i = 0; i < times; i++)
			{
				double[][] d = TrainingData();
				p.FeedForward(d[0]);
				p.BackPropagation(d[1]);

				Console.WriteLine($"training data [{d[0][0]}, {d[0][1]} = {d[1][0]}] -> {p.GetNeuronOutput(p.NumOfLayers - 1, 0).ToString("0.000")}");
			}
		}

		static void FullInfo(Perceptron p)
		{
			StringBuilder result = new StringBuilder();
			result.AppendLine(p.Name);

			for (int layer = 0; layer < p.NumOfLayers; layer++)
			{
				result.AppendLine("Layer: " + layer);

				for (int neuron = 0; neuron < p.GetNumOfNeurons(layer); neuron++)
				{
					result.Append($"N({layer}, {neuron}) = {p.GetNeuronOutput(layer, neuron).ToString("0.00")}");

					if (layer < p.NumOfLayers - 1)
					{
						result.Append($" [");
						for (int connection = 0; connection < p.GetNumOfNeurons(layer + 1, false); connection++)
						{
							result.Append($"{p.GetConnectionWeight(layer, neuron, connection).ToString("0.000")}");
							if (connection < p.GetNumOfNeurons(layer + 1, false) - 1)
								result.Append(" & ");
						}
						result.Append($"]");
					}

					if (neuron < p.GetNumOfNeurons(layer) - 1)
						result.Append(" | ");
				}

				result.AppendLine();
			}

			Console.WriteLine(result);
		}
	}
}
