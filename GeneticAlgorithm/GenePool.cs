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
        IChromosome Crossover(IChromosome p1, IChromosome p2);

        /// <summary>
        /// If the end condition is satisfied, stop, and return the best solution in current population
        /// </summary>
        bool Satisfied();

        IChromosome BestChromosome { get; }

        IChromosomeFactory ChromosomeFactory { get; set; }

		double GetFitness(IChromosome c);
	}

    public abstract class GenePool : IGenePool
    {
        IList<IChromosome> _pool;
        int _poolSize;

        public void Initialize(int poolSize)
        {
            _pool = new List<IChromosome>();
            _poolSize = poolSize;
            for (int i = 0; i < _poolSize; i++)
            {
                _pool.Add(ChromosomeFactory.CreateCromosome());
            }
        }

        public IChromosome Crossover(IChromosome p1, IChromosome p2)
        {
            throw new NotImplementedException();
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
                retirm _pool.Min();
            }
        }

        [Inject]
        public IChromosomeFactory ChromosomeFactory { get; set; }


        public bool Satisfied()
        {
            throw new NotImplementedException();
        }

        public IChromosome Select()
        {
            var rnd = new Random();
            int idx = -1;
            do { idx = rnd.Next(0, _poolSize); } while (rnd.NextDouble() > _pool[idx].Fitness);
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
	}
}
