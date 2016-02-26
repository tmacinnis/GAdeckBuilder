using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SQLite;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Terminations;
using GeneticSharp.Domain.Randomizations;
using GeneticSharp.Domain;

namespace GAdeckbuilder
{
    class Program
    {

        static public List<Card> CardPool = new List<Card>();
        static void Main(string[] args)
        {
            SQLiteConnection dbConnect;
            SQLiteCommand cmd;
            SQLiteDataReader dbread = null;
            string inputstring;
            string cmc = ""; string power = "";
            StreamReader inputReader = new StreamReader("C:/Users/Jake/Documents/Visual Studio 2015/Projects/GAdeckbuilder/input.txt");
            dbConnect = new SQLiteConnection("Data Source=C:/Users/Jake/Documents/Visual Studio 2015/Projects/GAdeckbuilder/mtg.db;");
            dbConnect.Open();
            string sql;
            bool white = false, blue = false, black = false, red = false, green = false;
            while (inputReader.Peek() != -1)
            {
                inputstring = inputReader.ReadLine();
                sql = "SELECT * FROM cards INNER JOIN colors ON cards.name = colors.name where cards.setcode = 'BFZ' AND colors.setcode = 'BFZ' AND cards.name = '" + inputstring + "';";
                // Console.WriteLine(sql);
                cmd = new SQLiteCommand(sql, dbConnect);
                dbread = cmd.ExecuteReader();
                while (dbread.Read())
                {
                    //Console.WriteLine(dbread[1].ToString());
                    white = false; blue = false; black = false; red = false; green = false;
                    if (Convert.ToBoolean(dbread["white"].ToString()))
                    {
                        white = true;
                    }
                    if (Convert.ToBoolean(dbread["blue"].ToString()))
                    {
                        blue = true;
                    }
                    if (Convert.ToBoolean(dbread["black"].ToString()))
                    {
                        black = true;
                    }
                    if (Convert.ToBoolean(dbread["red"].ToString()))
                    {
                        red = true;
                    }
                    if (Convert.ToBoolean(dbread["green"].ToString()))
                    {
                        green = true;
                    }
                    // Console.WriteLine(dbread["cmc"].ToString());
                    cmc = dbread["cmc"].ToString();
                    //Console.WriteLine(cmc);
                    power = dbread["PowerLevel"].ToString();
                    //Console.WriteLine(power);
                }
                // Console.WriteLine(inputstring + white + blue + red + black + green + cmc + power);
                CardPool.Add(new Card(inputstring, white, blue, black, red, green, int.Parse(cmc), int.Parse(power)));
            }
            inputReader.Close();
            dbread.Close();
            dbConnect.Close();


            foreach (Card c in CardPool)
            {
                //   Console.WriteLine(c.White);
            }
            Console.ReadLine();

            var selection = new EliteSelection();
            var crossover = new OnePointCrossover();
            var mutation = new ReverseSequenceMutation();
            var fitness = new DeckFitness();
            var chromosome = new DeckChromosome();
            var population = new Population(400, 400, chromosome);

            var ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
            ga.Termination = new FitnessStagnationTermination(4000);

            ga.CrossoverProbability = 0.80f;
            ga.MutationProbability = 1.00f;
            Console.WriteLine("GA running...");
            ga.Start();

            printDecklist(ga.BestChromosome);
            Console.WriteLine("Number of Generations " + ga.GenerationsNumber);
            Console.ReadLine();
            Console.ReadLine();
        }
        static void printDecklist(IChromosome chromosome)
        {
            int cardcount = 0;
            int power = 0;
            int cmc = 0;
            for (int i = 0; i < CardPool.Count; i++)
            {
                if (chromosome.GetGenes()[i].Value.Equals(1))
                {
                    var currentcard = Program.CardPool[i];
                    cardcount++;
                    power += currentcard.PowerLevel;
                    Console.WriteLine(CardPool[i].toString());
                    cmc += currentcard.ConvertedManaCost;
                }
            }
            Console.WriteLine("Card count: {0} Total Power: {1} Fitness {2}, Average CMC: {3}", cardcount, power, chromosome.Fitness, cmc / cardcount);
        }
    }
}
