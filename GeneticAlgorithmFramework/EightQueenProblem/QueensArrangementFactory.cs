using GeneticAlgorithm;
using GeneticAlgorithmFramework.EightQueenProblem;
using System.Collections.Generic;

namespace GeneticAlgorithmFramework
{
	public class QueensArrangementFactory : IChromosomeFactory
	{
		int _boardSize;
		public QueensArrangementFactory(int boardSize = 8)
		{
			_boardSize = boardSize;
		}

		public IChromosome CreateCromosome()
		{
			var g = new List<Digit>();
			for (int i = 0; i < _boardSize; i++)
			{
				g.Add(new Digit(1, _boardSize));
			}
			return new QueensArrangement(g);
		}
	}
}
