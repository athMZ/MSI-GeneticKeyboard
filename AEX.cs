using MSI_GeneticKeyBoard.Interfaces;

namespace MSI_GeneticKeyBoard
{
    public class AEX : ISplitOperator
    {
        public char[] Split(Element parent)
        {
            char[][] parents =
            {
                new char[parent.Chromosome.Length],
                new char[parent.Chromosome.Length]
            };

            parent.Chromosome.CopyTo(parents[0], 0);
            parent.Partner?.Chromosome.CopyTo(parents[1], 0);

            var child = new List<char>();
            var letterPos = -1;
            var oscillator = -1;

            foreach (var t in parents[0])
            {
                int parent1;
                int parent2;
                if (oscillator < 0)
                {
                    parent1 = 0;
                    parent2 = 1;
                }
                else
                {
                    parent1 = 1;
                    parent2 = 0;
                }

                letterPos = (letterPos + 1) % parents[0].Length;
                var addedLetter = parents[parent2][letterPos];

                var index = 0;
                while (addedLetter == ' ')
                {
                    addedLetter = parents[parent2][index];
                    index++;
                }

                child.Add(addedLetter);

                letterPos = Array.IndexOf(parents[parent1], addedLetter);

                parents[0][Array.IndexOf(parents[0], addedLetter)] = ' ';
                parents[1][Array.IndexOf(parents[1], addedLetter)] = ' ';

                oscillator = -oscillator;
            }

            return child.ToArray();
        }
    }
}