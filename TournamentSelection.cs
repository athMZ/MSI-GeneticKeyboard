using MSI_GeneticKeyBoard.Interfaces;

namespace MSI_GeneticKeyBoard
{
    public class TournamentSelection : IPartnerSelector
    {
        private int CandidateCount { get; set; } = Params.DefaultCandidateCount;

        public Func<int>? CandidateCountModifier { get; }

        public TournamentSelection()
        {
        }

        public TournamentSelection(Func<int> candidateCountModifier)
        {
            CandidateCountModifier = candidateCountModifier;
        }

        public void SelectPartners(List<Element> elementPopulation)
        {
            Random rnd = new();

            if (CandidateCountModifier != null)
                CandidateCount = CandidateCountModifier();

            Element? bestCandidate = new(Params.DefaultMax);
            elementPopulation.ForEach(x =>
            {
                for (var i = 0; i < CandidateCount; i++)
                {
                    var candidate = elementPopulation[rnd.Next(elementPopulation.Count)];

                    if (candidate == x) continue;
                    if (!(candidate.Fitness < bestCandidate?.Fitness)) continue;

                    bestCandidate = candidate;
                }

                x.Partner = bestCandidate;
            });
        }
    }
}