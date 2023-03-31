using MSI_GeneticKeyBoard;

namespace MSI_GeneticKeyboard
{
    internal class Program
    {
        private static void Main()
        {
            Population population = new(Fitness.CalculateBasicFitness);

            population.CreatePopulation();
        }
    }
}