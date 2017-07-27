using GeneticAlgorithm;
using GeneticAlgorithmFramework.EightQueenProblem;
using System.Collections.Generic;
using System.Text;
using System;

namespace GeneticAlgorithmFramework.EightQueenProblem
{
	public class QueensSetup : Chromosome<Digit>
	{
		public QueensSetup(IList<Digit> g) : base(g) { }

		public override string ToString()
		{
			var result = new StringBuilder();
			for (int i = 0; i < this.GetLenght(); i++)
			{
				result.Append(new string('-', _genes[i].Value - 1));
				result.Append("Q");
				result.Append(new string('-', _genes[i].UpperBound - _genes[i].Value));
				result.AppendLine();
			}
			return result.ToString();
		}

		public override double Fitness
		{
			get
			{
				return GetFitness();
				//TODO: find a way to hash already calculated fitnesses
			}
		}

		/// <summary>
		/// Each queen can threat maximum of 6 other queens as each queen is in separate row.
		/// The maximum number of pair of queens threatening each other is 28 = (7 + 6 + ... + 1)
		/// The fitness is 1/number of threat pairs.
		/// </summary>
		/// <returns></returns>
		private double GetFitness()
		{
			double hits = 0;
			for (int i = 0; i < _genes.Count; i++)
			{
				for (int j = i + 1; j < _genes.Count; j++)
				{
					if (_genes[i].Value == _genes[j].Value || Math.Abs(_genes[i].Value - _genes[j].Value) == j - i)
						hits++;
				}
			}
			return 1 - hits * (1.0 / 28.0);
		}

		public override Chromosome<Digit> Create(IList<Digit> g)
		{
			return new QueensSetup(g);
		}
	}
}
