namespace MSI_GeneticKeyBoard
{
    public class Population
    {
        private List<Element> PopulationList { get; set; } = new();

        private Func<char[], double> FitnessCalculator { get; }

        public Population(Func<char[], double> fitnessCalculator)
        {
            FitnessCalculator = fitnessCalculator;
        }

        public void CreatePopulation()
        {
            for (var i = 0; i < Params.PopulationSize; i++)
            {
                PopulationList.Add(new Element());
            }
        }

        public void CalculatePopulationFitness()
        {
            foreach (var element in PopulationList)
            {
                element.Fitness = FitnessCalculator(element.Chromosome);
            }
        }

        public Element GetBestElement()
        {
            return PopulationList.OrderBy(x => x.Fitness).First();
        }

        public double GetAverageFitness()
        {
            return PopulationList.Average(x => x.Fitness);
        }
    }
}
