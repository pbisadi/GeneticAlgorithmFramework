using GeneticAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmFramework.TravelingSalesman
{
	public class TripPool : GenePool
	{
		static List<City> _cities;

		public TripPool(int poolSize, IChromosomeFactory factory) : base(poolSize, factory)
		{
			_cities = new List<City>();

			_cities.Add(new City() { Name = "A1", X = -0.5, Y = 2.0 });
			_cities.Add(new City() { Name = "A2", X = 0.0, Y = 2.0 });
			_cities.Add(new City() { Name = "A3", X = 0.5, Y = 2.0 });

			_cities.Add(new City() { Name = "B1", X = 2.0, Y = 0.5 });
			_cities.Add(new City() { Name = "B2", X = 2.0, Y = 0.0 });
			_cities.Add(new City() { Name = "B3", X = 2.0, Y = -0.5 });

			_cities.Add(new City() { Name = "C1", X = -0.5, Y = -2.0 });
			_cities.Add(new City() { Name = "C2", X = 0.0, Y = -2.0 });
			_cities.Add(new City() { Name = "C3", X = 0.5, Y = -2.0 });

			_cities.Add(new City() { Name = "D1", X = -2.0, Y = -0.5 });
			_cities.Add(new City() { Name = "D2", X = -2.0, Y = 0.0 });
			_cities.Add(new City() { Name = "D3", X = -2.0, Y = 0.5 });
		}

		public TripPool(int poolSize, IChromosomeFactory factory, List<City> cities) : base(poolSize, factory)
		{
			_cities = cities;
		}

		public static List<City> Cities { get { return _cities; } }
	}
}
