using MSI_GeneticKeyBoard.Interfaces;

namespace MSI_GeneticKeyBoard
{
    public class Population
    {
        private List<Element> ElementPopulation { get; set; } = new();

        public static int CurrentGeneration { get; set; } = 0;

        private IFitness FitnessCalculator { get; }
        private IPartnerSelector PartnerSelector { get; }
        private ISplitOperator SplitOperator { get; }

        private IProgressLogger? ProgressLogger { get; set; }

        public Population(IFitness fitnessCalculator, IPartnerSelector partnerSelector, ISplitOperator splitOperator)
        {
            FitnessCalculator = fitnessCalculator;
            PartnerSelector = partnerSelector;
            SplitOperator = splitOperator;
        }

        public void SetProgressLogger(IProgressLogger logger)
        {
            ProgressLogger = logger;
        }

        public void CreatePopulation()
        {
            for (var i = 0; i < Params.PopulationSize; i++)
                ElementPopulation?.Add(new Element());
        }

        public void CalculatePopulationFitness()
        {
            foreach (var element in ElementPopulation)
                element.Fitness = FitnessCalculator.CalculateFitness(element.Chromosome);
        }

        public Element GetBestElement() => ElementPopulation.OrderBy(x => x.Fitness).First();

        public double GetAverageFitness() => ElementPopulation.Average(x => x.Fitness);

        public double GetBestFitness() => GetBestElement().Fitness;

        private int GetPreserveCount() => (int)(ElementPopulation.Count * Params.ElitePercentage);

        public IEnumerable<Element> GetBestElements()
        {
            if (Params.PreserveBestParents == false) return Enumerable.Empty<Element>();

            var elementCount = (int)(ElementPopulation.Count * Params.ElitePercentage);

            return ElementPopulation
                .OrderBy(x => x.Fitness)
                .Take(elementCount);
        }
        private IEnumerable<Element> CreateChildren()
        {
            var childrenCount = ElementPopulation.Count;

            if (Params.PreserveBestParents) childrenCount -= GetPreserveCount();

            return ElementPopulation
                .GetRange(0, childrenCount)
                .Select(x => new Element()
                {
                    Chromosome = SplitOperator.Split(x)
                });
        }

        public void GenerateNextPopulation()
        {
            List<Element> nextGeneration = new();

            CalculatePopulationFitness();
            PartnerSelector.SelectPartners(ElementPopulation);

            nextGeneration.AddRange(GetBestElements());
            nextGeneration.AddRange(CreateChildren());

            ProgressLogger?.LogProgress(this);

            ElementPopulation = nextGeneration;
            CurrentGeneration++;
        }
    }
}