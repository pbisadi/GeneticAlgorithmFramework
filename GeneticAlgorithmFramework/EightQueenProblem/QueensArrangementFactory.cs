using GeneticAlgorithm;
using GeneticAlgorithmFramework.EightQueenProblem;
using System.Collections.Generic;
using System;

namespace GeneticAlgorithmFramework
{
	public class QueensArrangementFactory: IChromosomeFactory<QueensArrangement, Digit>
	{
		int _boardSize;
		public QueensArrangementFactory(int boardSize = 8)
		{
			_boardSize = boardSize;
		}

		QueensArrangement IChromosomeFactory<QueensArrangement, Digit>.CreateChromosome()
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
