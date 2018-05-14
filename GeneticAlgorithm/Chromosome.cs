using Algorithm.Randomization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace GeneticAlgorithm
{
	public interface IChromosome<G> : IEnumerable<G>, IComparable<IChromosome<G>> where G : IGene
	{
		IChromosome<G> Crossover(IChromosome<G> other);
		IChromosome<G> Mutate();
		int GetLenght();
		double Fitness { get; }
	}

	public interface IChromosomeFactory<C, G> where C : IChromosome<G> where G : IGene
	{
		C CreateChromosome();
	}

	public interface IGene
	{
		void Mutate();
		IGene Clone();
	}

	public class Chromosome<G> : IChromosome<G> where G : IGene
	{
		public Chromosome<G> mom;
		public Chromosome<G> dad;

		static Random _rnd = new Random();

		protected IList<G> _genes;
		protected int[] _cross; // keeps the track of crosses

		public Chromosome(IList<G> genes)
		{
			_genes = genes;
			_cross = new int[_genes.Count-1];
			for (int i = 0; i < _cross.Length; i++) { _cross[i] = 1; }
		}

		public int GetLenght() { return _genes.Count(); }

		public IChromosome<G> Crossover(IChromosome<G> other)
		{
			Debug.Assert(this.GetLenght() == other.GetLenght(), "Two chromosomes must have the same length.");
			Debug.Assert(other is Chromosome<G>, "Two chromosome must be exact same type.");

			Chromosome<G> _other = (Chromosome<G>)other;

			var g = new List<G>();
			int cross_idx = GetCrossIndex();
			for (int i = 0; i < GetLenght(); i++)
			{
				IGene new_gene = i < cross_idx ? _genes[i].Clone() : _other._genes[i].Clone();
				g.Add((G)new_gene);
			}

			var offspring = this.Create(g);
			//offspring.mom = this;
			//offspring.dad = _other;
			Debug.WriteLine(String.Format("Crossover {0} & {1} => {2}", this.ToString(), other.ToString(), offspring.ToString()));
			return offspring;
		}

		private int GetCrossIndex()
		{
			int idx = _cross.WeightedPick();
			_cross[idx]++;
			return idx;
		}

		public virtual IChromosome<G> Mutate()
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
		public virtual Chromosome<G> Create(IList<G> g)
		{
			return new Chromosome<G>(g);
		}

		public int CompareTo(IChromosome<G> other)
		{
			return this.Fitness.CompareTo(other.Fitness);
		}

		public IEnumerator<G> GetEnumerator()
		{
			return _genes.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		public virtual double Fitness { get;}
	}
}
