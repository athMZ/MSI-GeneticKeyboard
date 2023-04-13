using MSI_GeneticKeyBoard.Interfaces;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using static MSI_GeneticKeyBoard.Interfaces.IProgressLogger;

namespace MSI_GeneticKeyBoard
{
    public class BasicProgressLogger : IProgressLogger
    {
        public List<GenerationInfo> AverageFitness { get; set; } = new();
        public List<GenerationInfo> BestFitness { get; set; } = new();
        public Element BestElement { get; set; } = new();

        public void LogProgress(Population population)
        {
            AverageFitness.Add(
                new GenerationInfo(
                    Population.CurrentGeneration,
                    population.GetAverageFitness()
                    )
                );

            BestFitness.Add(
                new GenerationInfo(
                    Population.CurrentGeneration,
                    population.GetBestFitness()
                    )
                );

            if (population.GetBestFitness() < BestElement.Fitness) BestElement = population.GetBestElement();

            Console.WriteLine($"Generation: {Population.CurrentGeneration,3}, Best Fitness: {population.GetBestFitness()}, Average fitness: {population.GetAverageFitness()}");
        }

        private static Bitmap GeneratePlotFromLogData(IReadOnlyCollection<GenerationInfo> logData, string title)
        {
            try
            {
                var plt = new ScottPlot.Plot(Params.PlotWidth, Params.PlotHeight);
                plt.AddScatter(
                    logData.Select(x => Convert.ToDouble(x.GenerationNumber)).ToArray(),
                    logData.Select(x => x.FitnessData).ToArray()
                    );

                plt.Title(title);
                plt.XLabel("Generation");
                plt.YLabel("Fitness");

                plt.AddAnnotation(
                    $"Generation count: {Params.GenerationCount}, " +
                        $"Population size: {Params.PopulationSize}, " +
                        $"Do mutations: {Params.DoMutations}",
                    -1, 0);

                return plt.GetBitmap();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Bitmap(0, 0);
            }
        }

        private static void CombinePlots(List<Bitmap> bitmaps, string data)
        {
            var width = bitmaps.Sum(x => x.Width);
            var height = Params.PlotHeight * bitmaps.Count / 2 + Params.TextBoxHeight;

            var background = new Rectangle(0, 0, width, height);
            var rect = new RectangleF(0, height - Params.TextBoxHeight, width, Params.TextBoxHeight);

            using var bmp = new Bitmap(width, height);
            using var gfx = System.Drawing.Graphics.FromImage(bmp);

            gfx.FillRectangle(Brushes.White, background);

            var j = 0;
            for (var i = 0; i < bitmaps.Count; i++)
            {
                gfx.DrawImage(
                    bitmaps[i],
                    (i % 2) * Params.PlotWidth,
                    (j % 2) * Params.PlotHeight
                    );

                if (i % 2 == 1) j++;
            }

            var stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            gfx.DrawString(data, new Font("Arial", 14), Brushes.Black, rect, stringFormat);
            bmp.Save(Params.PlotPath);
        }

        public static void OpenWithDefaultProgram(string path)
        {
            using var fileOpener = new Process();

            fileOpener.StartInfo.FileName = "explorer";
            fileOpener.StartInfo.Arguments = "\"" + path + "\"";
            fileOpener.Start();
        }

        public static string FormatChromosome(char[] chromosome)
        {
            var sb = new StringBuilder();

            for (var i = 0; i < chromosome.Length; i++)
            {
                if (i % 10 == 0) sb.Append('\n');
                sb.Append($"{chromosome[i]} ");
            }

            return sb.ToString();
        }

        public static void DisplayChromosome(char[] chromosome)
        {
            Console.WriteLine(FormatChromosome(chromosome));
            Console.WriteLine();

        }

        public void CreateSummary()
        {
            var minAverageFitness = AverageFitness
                .MinBy(x => x.FitnessData).FitnessData;

            var maxAverageFitness = AverageFitness
                .MaxBy(x => x.FitnessData).FitnessData;

            var minBestFitness = BestFitness
                .MinBy(x => x.FitnessData).FitnessData;

            var maxBestFitness = BestFitness
                .MaxBy(x => x.FitnessData).FitnessData;

            Console.WriteLine("\n------");

            StringBuilder sb = new();

            var averageDiff = maxAverageFitness - minAverageFitness;
            var bestDiff = maxBestFitness - minBestFitness;

            sb.Append($"Average fitness diff: {averageDiff.ToString("0.###")}, ");
            sb.Append($"Best fitness diff: {bestDiff.ToString("0.###")}");
            Console.WriteLine(sb.ToString());

            DisplayChromosome(BestElement.Chromosome);

            Bitmap[] bmps =
            {
                GeneratePlotFromLogData(AverageFitness, "Average Fitness for generation"),
                GeneratePlotFromLogData(BestFitness, "Best Fitness for generation"),
            };

            sb.Append('\n' + FormatChromosome(BestElement.Chromosome));

            CombinePlots(bmps.ToList(), sb.ToString());
            OpenWithDefaultProgram(Params.PlotPath);
        }
    }
}