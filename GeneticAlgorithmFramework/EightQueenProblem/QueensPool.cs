using GeneticAlgorithm;

namespace GeneticAlgorithmFramework.EightQueenProblem
{
	public class QueensPool : GenePool
	{
		public QueensPool(int poolSize, QueensArrangementFactory f) : base(poolSize, f) { }
	}
}
