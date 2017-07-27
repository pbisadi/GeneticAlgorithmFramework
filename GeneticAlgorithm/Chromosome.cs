using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
	public interface IChromosome : IComparable<IChromosome>
	{
		IChromosome Crossover(IChromosome other);
		IChromosome Mutate();
		int GetLenght();
		double Fitness { get; }
	}

	public interface IChromosomeFactory
	{
		IChromosome CreateCromosome();
	}

	public interface IGene
	{
		void Mutate();
	}

	public class Chromosome<T> : IChromosome where T : IGene
	{
		public Chromosome<T> mom;
		public Chromosome<T> dad;

		static Random _rnd = new Random();

		protected IList<T> _genes;

		public Chromosome(IList<T> genes)
		{
			_genes = genes;
		}

		public int GetLenght() { return _genes.Count(); }

		public IChromosome Crossover(IChromosome other)
		{
			Debug.Assert(this.GetLenght() == other.GetLenght(), "Two chromosomes must have the same lenght.");
			Debug.Assert(other is Chromosome<T>, "Two chromosome must be exact same type.");

			Chromosome<T> _other = (Chromosome<T>)other;

			var g = new List<T>();
			int cross_idx = _rnd.Next(0, GetLenght()+1);
			for (int i = 0; i < GetLenght(); i++)
			{
				g.Add(i < cross_idx ? _genes[i] : _other._genes[i]);
			}

			var offspring = this.Create(g);
			offspring.mom = this;
			offspring.dad = _other;
			return Create(g);
		}

		public IChromosome Mutate()
		{
			if (_genes != null || _genes.Count() > 0)
				_genes[_rnd.Next(0, GetLenght())].Mutate() ;  //TODO: Mutation of just one gene might not be enough.
			return this;
		}

		/// <summary>
		/// Override to create your own chromosomes in subclass
		/// </summary>
		/// <param name="g">A list of genes to create a new chromosome</param>
		/// <returns></returns>
		public virtual Chromosome<T> Create(IList<T> g)
		{
			return new Chromosome<T>(g);
		}

		public int CompareTo(IChromosome other)
		{
			return this.Fitness.CompareTo(other.Fitness);
		}

		public virtual double Fitness { get;}
	}
}
