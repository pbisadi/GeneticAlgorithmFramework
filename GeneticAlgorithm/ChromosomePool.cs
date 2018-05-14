using Algorithm.Randomization;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
	public interface IChromosomePool<C, G> where C : IChromosome<G> where G : IGene
	{
		/// <summary>
		///  Repeats the following steps until the new population is generated from previous generation
		///     Selection
		///     Crossover
		///     Mutation
		///     Accepting
		/// </summary>
		void CreateNextGeneration();

		/// <summary>
		/// Returns an IChromosome which has higher fitness than threshold or
		/// returns the best IChromosome after maxIteration generations.
		/// </summary>
		/// <param name="maxIteration">Max number of generations that will be created to find the best solution</param>
		/// <param name="threshold">return the result if there is an IChromosome with fitness higher than threshold</param>
		/// <returns></returns>
		C GenerateSolution(int maxIteration, double threshold);

		/// <summary>
		/// Calls GeneratePopulation to initialize the pool
		/// </summary>
		/// <param name="poolSize">Number of chromosome in the pool</param>
		void Initialize(int poolSize);

		/// <summary>
		/// Select two parent chromosomes from a population according to their fitness(the better fitness, the bigger chance to be selected)
		/// </summary>
		C Select();

		/// <summary>
		/// With a crossover probability cross over the parents to form new offspring(children). If no crossover was performed, offspring is the exact copy of parents.
		/// </summary>
		C Crossover(C mom, C dad);

		C BestChromosome { get; }

		IChromosomeFactory<C, G> ChromosomeFactory { get; set; }
	}

	public abstract class ChromosomePool<C, G> : IChromosomePool<C, G> where C : IChromosome<G> where G : IGene
	{
		List<C> _pool;
		int _poolSize;
		static Random _rnd = new Random();
		private int poolSize;


		public ChromosomePool(int poolSize, IChromosomeFactory<C,G> factory)
		{
			this.ChromosomeFactory = factory;
			Initialize(poolSize);
		}

		public void Initialize(int poolSize)
		{
			_pool = new List<C>();
			_poolSize = poolSize;
			for (int i = 0; i < _poolSize; i++)
			{
				_pool.Add(ChromosomeFactory.CreateChromosome());
			}
		}

		public C Crossover(C mom, C dad)
		{
			return (C)mom.Crossover(dad);
		}

		public void CreateNextGeneration()
		{
			var nextGeneration = new List<C>();
			while (nextGeneration.Count < _poolSize)
			{
				///Two chromosomes are selected and the result of their 
				///crossover is mutated and added to the next generation population
				nextGeneration.Add((C)Select().Crossover(Select()).Mutate());
			}

			_pool.AddRange(nextGeneration);
			_pool.Sort();
			_pool = _pool.GetRange(_poolSize, _poolSize); // get the top half
			//TODO: Replace picking top half with random weighted half selection
			//See:
			//https://stackoverflow.com/questions/2140787/select-k-random-elements-from-a-list-whose-elements-have-weights
		}

		public C BestChromosome
		{
			get
			{
				return _pool.Max();
			}
		}

		[Inject]
		public IChromosomeFactory<C, G> ChromosomeFactory { get; set; }
		
		public C Select()
		{
			return _pool[_pool.WeightedPick(c => c.Fitness)];
		}

		public C GenerateSolution(int maxIteration, double threshold)
		{
			for (int i = 0; i < maxIteration; i++)
			{
				if (this.BestChromosome.Fitness >= threshold) return this.BestChromosome;
				Console.WriteLine(i + ": "+ this.BestChromosome.Fitness);
				Console.WriteLine(this.BestChromosome.ToString());
				CreateNextGeneration();
			}
			return this.BestChromosome;
		}
	}
}
