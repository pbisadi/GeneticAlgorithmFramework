using GeneticAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmFramework.EightQueenProblem
{
	public class Digit : IGene
	{
		public Digit(int lowerBound = 0, int upperBound = 9)
		{
			this.LowerBound = lowerBound;
			this.UpperBound = upperBound;
			Mutate();
		}

		int _value;
		public int Value
		{
			get
			{
				return _value;
			}
			set
			{
				if (value < LowerBound) _value = LowerBound;
				else if (value > UpperBound) _value = UpperBound;
				else _value = value;
			}
		}

		public void Mutate()
		{
			var rnd = new Random();
			this.Value = rnd.Next(LowerBound, UpperBound + 1);
		}

		public int LowerBound { get; set; }
		public int UpperBound { get; set; }
	}
}
