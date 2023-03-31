namespace MSI_GeneticKeyBoard
{
    public class Fitness
    {
        public static double CalculateBasicFitness(char[] chromosome)
        {
            //Base fitness

            return chromosome
                .Select((c, i) => InputData.LetterFrequency[c] * InputData.LetterCost[i])
                .Sum();
        }
    }
}
