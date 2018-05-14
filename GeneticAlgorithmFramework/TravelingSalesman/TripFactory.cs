using Algorithm.Sort;
using GeneticAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmFramework.TravelingSalesman
{
	public class TripFactory : IChromosomeFactory<Trip, LocationIndex>
	{
		private int _stepsCount;
		public TripFactory(int stepsCount)
		{
			_stepsCount = stepsCount;
		}

		public Trip CreateChromosome()
		{
			var t = new List<LocationIndex>();
			for (int i = 0; i < _stepsCount; i++)
			{
				t.Add(new LocationIndex(0, _stepsCount - 1) { Value = i });
			}
			Shuffler.Shuffle<LocationIndex>(t);
			return new Trip(t);
		}
	}
}
