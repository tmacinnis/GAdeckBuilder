using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticSharp;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Terminations;
using GeneticSharp.Domain.Randomizations;


namespace GAdeckbuilder
{
    public class DeckChromosome : ChromosomeBase
    {
        public DeckChromosome() : base(Program.CardPool.Count)
        {
           for (int x = 0; x < Program.CardPool.Count; x++) {
               ReplaceGene(x, GenerateGene(x));
            }
        }

        public override Gene GenerateGene(int geneIndex)
        {
            // Generate a gene base on my problem chromosome representation.
            var x = RandomizationProvider.Current.GetInt(0, 2);

            return new Gene(x);
        }

        public override IChromosome CreateNew()
        {
            return new DeckChromosome();
        }
    }
}