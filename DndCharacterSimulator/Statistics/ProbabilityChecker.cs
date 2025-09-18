using DndCharacterSimulator.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DndCharacterSimulator.Statistics
{
    internal class ProbabilityChecker
    {
        public static void SimulateProbabilityDistributionSummation()
        {
            var generator = new CharacterGeneratorFactory();

            var initParentStatBasic = new List<int>() { 15, 14, 13, 12, 10, 8 };

            var probabilityDistribution = generator.GenerateProbabilityDistributionStandardSummation(initParentStatBasic,initParentStatBasic);

            PrintStatisticResult(generator, probabilityDistribution);
        }

        public static void SimulateProbabilityDistributionExponential()
        {
            var generator = new CharacterGeneratorFactory();

            var initParentStatBasic = new List<int>() { 15, 14, 13, 12, 10, 8 };

            var probabilityDistribution = generator.GenerateProbabilityDistributionExponential(initParentStatBasic, initParentStatBasic);

            PrintStatisticResult(generator, probabilityDistribution);
        }


        private static void PrintStatisticResult(CharacterGeneratorFactory generator, List<int> probabilityDistribution)
        {
            var statLines = new List<List<int>>();
            var sampleSize = 100;
            var averageStatLine = new List<int>() { 0, 0, 0, 0, 0, 0 };
            for (var i = 0; i < sampleSize; i++)
            {
                var generatedLine = generator.GenerateStatLine(72, probabilityDistribution, 8, 18);
                for (var j = 0; j < generatedLine.Count; j++)
                {
                    averageStatLine[j] += generatedLine[j];
                }

                statLines.Add(generatedLine);
            }
            averageStatLine = averageStatLine.Select(x => x / sampleSize).ToList();


            Console.WriteLine("AverageLine:");
            Console.WriteLine(string.Join(',', averageStatLine));

            Console.WriteLine("All Lines");
            foreach (var line in statLines)
            {
                Console.WriteLine(string.Join(',', line));
            }
        }
    }
}
