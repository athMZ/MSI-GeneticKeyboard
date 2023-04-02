namespace MSI_GeneticKeyBoard
{
    public class Element
    {
        public char[] Chromosome { get; set; } = new char[InputData.ChromosomeLength];

        public double Fitness { get; set; }

        public Element? Partner { get; set; }

        public Element()
        {
            Random rnd = new();
            Chromosome = InputData.AvailableChars
                .OrderBy(x => rnd.Next())
                .ToArray();
        }

        public Element(int sampleFitness)
        {
            Fitness = sampleFitness;
        }
    }
}
