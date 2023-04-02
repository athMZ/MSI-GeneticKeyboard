namespace MSI_GeneticKeyBoard.Interfaces;

public interface IProgressLogger
{
    readonly struct GenerationInfo
    {
        public readonly int GenerationNumber;
        public readonly double FitnessData;

        public GenerationInfo(int generationNumber, double fitnessData)
        {
            this.GenerationNumber = generationNumber;
            this.FitnessData = fitnessData;
        }
    }

    List<GenerationInfo> AverageFitness { get; set; }
    List<GenerationInfo> BestFitness { get; set; }

    Element BestElement { get; set; }

    void LogProgress(Population population);
    void CreateSummary();
}