using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
	public interface IGenePool
	{
		/// <summary>
		///  Repeats the following steps until the new population is generated from previous generation
		///     Selection
		///     Crossover
		///     Mutaion
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
		IChromosome GenerateSolution(int maxIteration, double threshold);

		/// <summary>
		/// Calls GeneratePopulation to initialize the pool
		/// </summary>
		/// <param name="poolSize">Number of chromosome in the pool</param>
		void Initialize(int poolSize);

		/// <summary>
		/// Select two parent chromosomes from a population according to their fitness(the better fitness, the bigger chance to be selected)
		/// </summary>
		IChromosome Select();

		/// <summary>
		/// With a crossover probability cross over the parents to form new offspring(children). If no crossover was performed, offspring is the exact copy of parents.
		/// </summary>
		IChromosome Crossover(IChromosome mom, IChromosome dad);

		IChromosome BestChromosome { get; }

		IChromosomeFactory ChromosomeFactory { get; set; }

		double GetFitness(IChromosome c);
	}

	public abstract class GenePool : IGenePool
	{
		IList<IChromosome> _pool;
		int _poolSize;

		public GenePool(int poolSize, IChromosomeFactory factory)
		{
			this.ChromosomeFactory = factory;
			Initialize(poolSize);
		}

		public void Initialize(int poolSize)
		{
			_pool = new List<IChromosome>();
			_poolSize = poolSize;
			for (int i = 0; i < _poolSize; i++)
			{
				_pool.Add(ChromosomeFactory.CreateCromosome());
			}
		}

		public IChromosome Crossover(IChromosome mom, IChromosome dad)
		{
			return mom.Crossover(dad);
		}

		public void CreateNextGeneration()
		{
			var nextGeneration = new List<IChromosome>();
			while (nextGeneration.Count < _poolSize)
			{
				///Two chromosomes are selected and the result of their 
				///crossover is mutated and added to the next generation population
				nextGeneration.Add(Select().Crossover(Select()).Mutate());
			}
			UpdateFitnessValues(nextGeneration);
			_pool = nextGeneration;
		}

		private void UpdateFitnessValues(List<IChromosome> nextGeneration)
		{
			foreach (var c in nextGeneration)
			{
				c.Fitness = GetFitness(c);
			}
		}

		public IChromosome BestChromosome
		{
			get
			{
				return _pool.Max();
			}
		}

		[Inject]
		public IChromosomeFactory ChromosomeFactory { get; set; }

		public IChromosome Select()
		{
			var rnd = new Random();
			int idx = -1;
			do { idx = rnd.Next(0, _poolSize); } while (rnd.NextDouble() < _pool[idx].Fitness);
			return _pool[idx];
		}

		/// <summary>
		/// Override this with your fitness rules
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		public virtual double GetFitness(IChromosome c)
		{
			return 0;
		}

		public IChromosome GenerateSolution(int maxIteration, double threshold)
		{
			for (int i = 0; i < maxIteration; i++)
			{
				if (this.BestChromosome.Fitness >= threshold) return this.BestChromosome;
				CreateNextGeneration();
			}
			return this.BestChromosome;
		}
	}
}
