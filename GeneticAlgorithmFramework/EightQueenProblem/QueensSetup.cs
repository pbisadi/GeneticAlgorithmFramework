using GeneticAlgorithm;
using GeneticAlgorithmFramework.EightQueenProblem;
using System.Collections.Generic;
using System.Text;

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
			return base.ToString();
		}

		double _fitness;
		string _hash;
		public override double Fitness
		{
			get
			{

			}
			set
			{

			}
		}
	}
}
