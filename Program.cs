using System.Diagnostics;
using MSI_GeneticKeyBoard;
using MSI_GeneticKeyBoard.Interfaces;

namespace MSI_GeneticKeyboard
{
    internal class Program
    {
        private static int CandidateCountModifier()
        {
            return Population.CurrentGeneration > Params.GenerationCount / 2 ? 40 : Params.DefaultCandidateCount;
        }

        private static void Main()
        {
            IProgressLogger logger = new BasicProgressLogger();

            Population population = new(
                new BasicFitness(),
                new TournamentSelection(CandidateCountModifier),
                new AEX()
                );

            population.SetProgressLogger(logger);

            population.CreatePopulation();

            for (var i = 0; i < Params.GenerationCount; i++)
            {
                population.GenerateNextPopulation();
            }

            logger.CreateSummary();

            Console.ReadLine();
        }
    }
}