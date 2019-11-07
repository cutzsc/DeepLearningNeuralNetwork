using System;
using DeepLearningNeuralNetwork;


namespace Launcher
{
	class Program
	{
		static void Main(string[] args)
		{
			Perceptron p = new Perceptron(new int[] { 2, 3, 1 }, 1,
				Functions.Sigmoid, Functions.SigmoidDerivative, -0.5, 0.5);

			Train(p, 5000);

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
				p.BackPropagation(d[1], 0.4, 0.85);

				double[] res = p.Result;

				if ((double)i / times > 0.97)
					Console.WriteLine($"Data: [{d[0][0]}, {d[0][1]}] -> {d[1][0]} ~ {res[0].ToString("0.000")}");
			}
		}
	}
}
