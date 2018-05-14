using GeneticAlgorithm;

namespace GeneticAlgorithmFramework.EightQueenProblem
{
	public class QueensPool : ChromosomePool<QueensArrangement, Digit>
	{
		public QueensPool(int poolSize, QueensArrangementFactory f) : base(poolSize, f) { }
	}
}
