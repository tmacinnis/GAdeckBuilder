using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Terminations;
using GeneticSharp.Domain.Randomizations;
using GeneticSharp.Domain.Fitnesses;

namespace GAdeckbuilder
{
    public class DeckFitness : IFitness
    {
        public double Evaluate (IChromosome chromosome)
        {
            var x = 0;
            var cardcount = 0;
            var power = 0.0;
            var colors = 0;
            var cmc = 0;
            bool white = false, blue = false, black = false, red = false, green = false;
            for (int i = 0; i < Program.CardPool.Count; i++)
            {
                if(chromosome.GetGenes()[i].Value.Equals(1))
                {
                    var currentcard = Program.CardPool[i];
                    cardcount++;
                    power += currentcard.PowerLevel;
                    cmc += currentcard.ConvertedManaCost;
                    if(currentcard.White)
                    {
                        if(white == false)
                        {
                            white = true;
                            colors++;
                        }
                    }
                    if (currentcard.Blue)
                    {
                        if (blue == false)
                        {
                            blue = true;
                            colors++;
                        }
                    }
                    if (currentcard.Black)
                    {
                        if (black == false)
                        {
                            black = true;
                            colors++;
                        }
                    }
                    if (currentcard.Red)
                    {
                        if (red == false)
                        {
                            red = true;
                            colors++;
                        }
                    }
                    if (currentcard.Green)
                    {
                        if (green == false)
                        {
                            green = true;
                            colors++;
                        }
                    }
                }
            }
            if (cardcount > 24)
            {
                x = (cardcount - 24);
            }
            if (cardcount < 21)
            {
                x = (21 - cardcount);
            }
            int averagecmc = cmc / cardcount;
            var cmcmod = 1;
            switch (averagecmc) {

                case 1: cmcmod = 4;
                        break;
                case 2: cmcmod = 2;
                        break;
                case 3: cmcmod = 1;
                        break;
                case 4: cmcmod = 1;
                        break;
                case 5: cmcmod = 2;
                        break;
                case 6: cmcmod = 4;
                        break;
               default: cmcmod = 5;
                        break;
            }
               
                return (power - (x * 1.5 * 10)) / (colors) / cmcmod;
        }
    }
}

