using MSI_GeneticKeyBoard.Interfaces;

namespace MSI_GeneticKeyBoard
{
    public class BasicFitness : IFitness
    {
        public double CalculateFitness(char[] chromosome)
        {
            return chromosome
                .Select((c, i) => InputData.LetterFrequency[c] * InputData.LetterCost[i])
                .Sum();
        }
    }
}
