using GeneticAlgorithm;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmFramework
{
    class Program
    {
        

        static void Main(string[] args)
        {
            using (IKernel kernel = new StandardKernel())
            {
                kernel.Bind<IGenePool>()
                              .To<GenePool>()
                              .WithConstructorArgument("poolSize", 100);

                kernel.Bind<IChromosome>()
                    .To<Chromosome<IntGene>>();

				var pool = kernel.Get<IGenePool>();
				
            }
        }
    }

    public class IntGene : IGene
    {
        public IntGene()
        {
            Mutate();
        }
        public int Value { get; set; }

        public void Mutate()
        {
            var rnd = new Random();
            this.Value = rnd.Next(1, 9);
        }
    }
}
