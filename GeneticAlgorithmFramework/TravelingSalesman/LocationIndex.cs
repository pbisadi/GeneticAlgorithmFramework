using GeneticAlgorithm;
using GeneticAlgorithmFramework.EightQueenProblem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmFramework.TravelingSalesman
{
	public class LocationIndex: IGene
	{
		static Random _rnd = new Random();
		public LocationIndex(int lowerBound, int upperBound)
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
			this.Value = _rnd.Next(LowerBound, UpperBound + 1);
		}

		public IGene Clone()
		{
			var other = new LocationIndex(this.LowerBound, this.UpperBound);
			other.Value = _value;
			return other;
		}

		public int LowerBound { get; set; }
		public int UpperBound { get; set; }
	}
}
