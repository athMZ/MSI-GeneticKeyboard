namespace MSI_GeneticKeyBoard
{
    public class InputData
    {
        public static readonly Dictionary<char, double> LetterFrequency = new()
        {
            {'a', 0.082},
            {'b', 0.015},
            {'c', 0.028},
            {'d', 0.043},
            {'e', 0.13},
            {'f', 0.022},
            {'g', 0.02},
            {'h', 0.061},
            {'i', 0.07},
            {'j', 0.0015},
            {'k', 0.0077},
            {'l', 0.04},
            {'m', 0.024},
            {'n', 0.067},
            {'o', 0.075},
            {'p', 0.019},
            {'q', 0.00095},
            {'r', 0.06},
            {'s', 0.063},
            {'t', 0.091},
            {'u', 0.028},
            {'v', 0.0098},
            {'w', 0.024},
            {'x', 0.0015},
            {'y', 0.02},
            {'z', 0.0074},
            {';', 0.0033},
            {'.', 0.01000},
            {',', 0.01000},
            {'/', 0.003 }
        };

        public static readonly List<double> LetterCost = new()
        {
            4, 2, 2, 3, 4, 4, 3, 2, 2, 4,
            1.5, 1, 1, 1, 3, 3, 1, 1, 1, 1.5,
            4, 4, 3, 2, 4, 4, 2, 3, 4, 4

        };

        public static readonly int ChromosomeLength = LetterFrequency.Count;

        public static readonly List<char> AvailableChars = LetterFrequency
            .Select(x => x.Key)
            .ToList();
    }
}