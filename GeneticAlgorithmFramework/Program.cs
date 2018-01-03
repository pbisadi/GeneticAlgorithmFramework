using GeneticAlgorithm;
using GeneticAlgorithmFramework.EightQueenProblem;
using GeneticAlgorithmFramework.TravelingSalesman;
using Ninject;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmFramework
{
	class Program
	{

		static void Main(string[] args)
		{
			//QueensProblem();
			TSP();
		}

		private static void TSP()
		{
			using (IKernel kernel = new StandardKernel())
			{
				kernel.Bind<IChromosomeFactory>()
					.To<TripFactory>()
					.WithConstructorArgument("stepsCount", 12);

				kernel.Bind<IGenePool>()
					.To<TripPool>()
					.WithConstructorArgument("poolSize", 400);

				kernel.Bind<IChromosome>()
					.To<Trip>();

				var pool = kernel.Get<IGenePool>();
				var result = pool.GenerateSolution(100, 1.0);
				Console.WriteLine(result.Fitness);
				Console.WriteLine(result.ToString());
				Console.ReadLine();
			}
		}

		private static void QueensProblem()
		{
			using (IKernel kernel = new StandardKernel())
			{
				kernel.Bind<IChromosomeFactory>()
					.To<QueensArrangementFactory>()
					.WithConstructorArgument("boardSize", 8);

				kernel.Bind<IGenePool>()
					.To<QueensPool>()
					.WithConstructorArgument("poolSize", 100);

				kernel.Bind<IChromosome>()
					.To<Chromosome<Digit>>();

				kernel.Bind<Chromosome<Digit>>()
					.To<QueensArrangement>()
					.WithConstructorArgument("size", 8);

				var pool = kernel.Get<IGenePool>();
				var result = pool.GenerateSolution(100, 1.0);
				Console.WriteLine(result.Fitness);
				Console.WriteLine(result.ToString());
				Console.ReadLine();
			}
		}
	}
}
