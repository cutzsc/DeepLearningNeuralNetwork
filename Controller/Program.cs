using System;
using System.Threading.Tasks;
using DeepLearningNeuralNetwork;


namespace Launcher
{
	class Program
	{
		static void Main(string[] args)
		{
			PerceptronInfo netInfo = PerceptronInfo.CreateClassicPerceptron(-0.5, 0.5, 2, 4, 1);
			Perceptron perceptron;
			bool positive;

		Start:
			if (YesOrNo("Create new perceptron? [y / n]: ", out positive))
				if (positive)
				{
					Console.Write("Enter layers count: ");
					int layersCount = StringToInt();
					if (layersCount < 3 || layersCount > 10)
						layersCount = 3;
					Console.WriteLine($"Layers count is {layersCount}");
					int[] net = new int[layersCount];
					net[0] = 2;
					Console.WriteLine($"Input layer have {net[0]} neurons");
					for (int i = 1; i < layersCount - 1; i++)
					{
						Console.Write($"Enter neurons count on layer №{i + 1}: ");
						int neuronsCount = StringToInt();
						net[i] = neuronsCount;
					}
					net[net.Length - 1] = 1;
					Console.WriteLine($"Output layer have {net[0]} neurons");
					netInfo = PerceptronInfo.CreateClassicPerceptron(-.5, .5, net);
				}

			perceptron = new Perceptron(netInfo);
			Console.Write("Enter train cycles count: ");
			int cycles = StringToInt();
			Train(perceptron, cycles);

			if (YesOrNo("Restart program [y / n]: ", out positive))
				if (positive)
					goto Start;

			Console.WriteLine("press enter to exit...");
			Console.ReadLine();
		}

		static bool YesOrNo(string dialogMessage, out bool positive)
		{
			Console.Write(dialogMessage);
			char input;
			if (char.TryParse(Console.ReadLine(), out input))
			{
				positive = input == 'y' || input == 'Y';
				return true;
			}
			positive = false;
			return false;
		}

		static int StringToInt()
		{
			bool isDone = false;
			int output = -1;
			do
			{
				try
				{
					output = Convert.ToInt32(Console.ReadLine());
					isDone = true;
				}
				catch { }
			} while (!isDone);
			return output;
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
				p.BackPropagation(d[1], 0.45, 0.85);
				double[] res = p.Result;

				if ((double)i / times > 0.97)
					Console.WriteLine($"[{d[0][0]}, {d[0][1]}] -> {res[0].ToString("0.000")} ~ {d[1][0]}");
			}
		}
	}
}
