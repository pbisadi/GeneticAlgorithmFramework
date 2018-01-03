using GeneticAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmFramework.TravelingSalesman
{
	public class Trip : Chromosome<LocationIndex>
	{
		int _stepCount;
		public Trip(IList<LocationIndex> genes) : base(genes) 
		{
			_stepCount = genes.Count;
		}

		public override double Fitness {
			get
			{
				double travelled_distance = 0;
				for (int i = 0; i < _genes.Count - 1; i++)
				{
					travelled_distance += Distance(
						TripPool.Cities[_genes[i].Value]
						, TripPool.Cities[_genes[i + 1].Value]);
				}

				var missed_cities = new List<int>();
				for (int i = 0; i < TripPool.Cities.Count; i++)
				{
					if (_genes.FirstOrDefault(g => g.Value == i) == null)
					{
						missed_cities.Add(i);
					}
				}

				if (missed_cities.Count > 0) travelled_distance += Distance(TripPool.Cities[_genes[_genes.Count - 1].Value], TripPool.Cities[missed_cities[0]]);

				for (int i = 0; i < missed_cities.Count -1; i++)
				{
					travelled_distance += 1 + Distance(	// penalty of extra steps
						TripPool.Cities[missed_cities[i]]
						, TripPool.Cities[missed_cities[i+1]]);
				}

				return 1 / (1 + travelled_distance);
			}
		}

		private double Distance(City a, City b)
		{
			return Math.Sqrt(Math.Pow((a.X - b.X), 2) + Math.Pow((a.Y - b.Y), 2));
		}

		public override IChromosome Mutate()
		{
			if (_genes != null || _genes.Count() > 0)
			{
				Random _rnd = new Random();
				var i = _rnd.Next(0, _genes.Count());
				var j = _rnd.Next(0, _genes.Count());
				var temp = _genes[i].Value;
				_genes[i] = new LocationIndex(0, _stepCount - 1) { Value = _genes[j].Value };
				_genes[j] = new LocationIndex(0, _stepCount - 1) { Value = _genes[i].Value };
			}

			return this;
		}

		public override string ToString()
		{
			return string.Join(".", _genes.Select(g => TripPool.Cities[g.Value].Name).ToArray());
		}

		public override Chromosome<LocationIndex> Create(IList<LocationIndex> g)
		{
			return new Trip(g);
		}
	}
}
