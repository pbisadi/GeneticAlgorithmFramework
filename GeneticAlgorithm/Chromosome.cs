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
        double Fitness { get; set; }
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
        IList<T> _genes;

        public Chromosome(int size)
        {

        }

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

            Random rnd = new Random();
            var result = new List<T>();
            int cross_idx = rnd.Next(0, GetLenght());
            for (int i = 0; i < GetLenght(); i++)
            {
                result.Add(i < cross_idx ? _genes[i] : _other._genes[i]);
            }

            return new Chromosome<T>(result);
        }

        public IChromosome Mutate()
        {
            Random rnd = new Random();
            if (_genes != null || _genes.Count() > 0)
                _genes[rnd.Next(0, GetLenght())].Mutate();  //TODO: Mutation of just one gene might not be enough.
            return this;
        }

		public int CompareTo(IChromosome other)
		{
			return this.Fitness.CompareTo(other.Fitness);
		}

		public double Fitness { get; set; }
    }
}
