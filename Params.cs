namespace MSI_GeneticKeyBoard
{
    public class Params
    {
        //Algorithm params
        public static int DefaultMax => 1_000_000;

        public static int PopulationSize => 300;
        public static int GenerationCount => 100;
        public static int DefaultCandidateCount => 4;
        public static double ElitePercentage => 0.10;
        public static bool PreserveBestParents => true;
        public static bool DoMutations => false;

        //Plot params
        public static int PlotWidth => 800;
        public static int PlotHeight => 600;
        public static int TextBoxHeight => 150;
        public static string PlotPath => @"plot.png";
    }
}