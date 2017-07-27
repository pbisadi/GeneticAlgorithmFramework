using GeneticAlgorithm;

namespace GeneticAlgorithmFramework.EightQueenProblem
{
	public class QueensPool : GenePool
	{
		public QueensPool(int poolSize, QueensSetupFactory f) : base(poolSize, f) { }
	}
}
