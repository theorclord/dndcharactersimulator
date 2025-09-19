using DndCharacterSimulator.Generators;
using DndCharacterSimulator.Models;
using DndCharacterSimulator.Statistics;

namespace DndCharacterSimulator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            ProbabilityCheckerGoup();

            CharacterGeneratorTest();

            ProbabilityBiasTest();

            // Small city with fullplate production test
            var fullPlate = new Item() { Name = "Full Plate", BasePrice = 1500, ProductionCost = 1 };
            var smith = new Building() { Name = "Smith", BaseProduction = 10, ProductionItems = new List<Item>() { fullPlate } };
            var populationGroup = new PopulationGroup(100, new StatLine(new int[] { 15, 14, 13, 12, 10, 8 }), Race.RaceType.Human);


            var raceDistribution = new Dictionary<Race.RaceType, int>();
            raceDistribution[Race.RaceType.Human] = 60;
            raceDistribution[Race.RaceType.Elf] = 10;
            raceDistribution[Race.RaceType.Dwarf] = 10;
            raceDistribution[Race.RaceType.Orc] = 2;
            raceDistribution[Race.RaceType.Dragonborn] = 3;
            raceDistribution[Race.RaceType.Halfling] = 5;

            var city = CityGeneratorFactory.GenerateCity(2000,raceDistribution);

            var raceSum = 0;
            foreach(var raceGroup in city.PopulationGroups)
            {
                raceSum += raceGroup.Individuals;
                Console.WriteLine($"Race: {raceGroup.RaceType}, {raceGroup.Individuals} ");
            }
            Console.WriteLine(raceSum);

            Console.ReadKey();
        }

        private static void ProbabilityCheckerGoup()
        {
            ProbabilityChecker.SimulateProbabilityDistributionSummation();
        }

        private static void CharacterGeneratorTest()
        {
            var characterGenerator = new CharacterGeneratorFactory();

            // Test of stat line generator
            var standardArray = new List<int> { 15, 14, 13, 12, 10, 8 };
            var standardStatSum = standardArray.Sum();
            for (var i = 0; i < 4; i++)
            {
                var statLine = characterGenerator.GenerateStatLine(standardStatSum, standardArray, 4, 18);

                Console.WriteLine("StatLine:");
                Console.WriteLine(string.Join(',', statLine.GetStatLineArray()));
                Console.WriteLine("StatSum:");
                Console.WriteLine(statLine.GetStatLineArray().Sum());
            }
        }

        private static void ProbabilityBiasTest()
        {
            var characterGenerator = new CharacterGeneratorFactory();

            var standardArray = new List<int> { 15, 14, 13, 12, 10, 8 };
            var standardStatSum = standardArray.Sum();

            // This might give an indication of needing a greate emphasis on stat difference
            var testProbility = new List<int> { 100, 20, 100, 20, 20, 50 };
            var probAbilityTestLine = characterGenerator.GenerateStatLine(standardStatSum, testProbility, 8, 18);

            Console.WriteLine("StatLine:");
            Console.WriteLine(string.Join(',', probAbilityTestLine.GetStatLineArray()));
            Console.WriteLine("StatSum:");
            Console.WriteLine(probAbilityTestLine.GetStatLineArray().Sum());

            // Test of probability distribution
            var statLine1 = characterGenerator.GenerateStatLine(standardStatSum, standardArray, 8, 18);
            var statLine2 = characterGenerator.GenerateStatLine(standardStatSum, standardArray, 8, 18);
            Console.WriteLine("StatLine1:");
            Console.WriteLine(string.Join(',', statLine1.GetStatLineArray()));
            Console.WriteLine("StatLine2:");
            Console.WriteLine(string.Join(',', statLine2.GetStatLineArray()));
            var probabilityDistribution = characterGenerator.GenerateProbabilityDistributionPolynomial(statLine1.GetStatLineArray(), statLine2.GetStatLineArray());
            Console.WriteLine("Probability Distribution:");
            Console.WriteLine(string.Join(",", probabilityDistribution));

            var childStatLine = characterGenerator.GenerateStatLine(standardStatSum, probabilityDistribution, 8, 18);
            Console.WriteLine("StatLine:");
            Console.WriteLine(string.Join(',', childStatLine.GetStatLineArray()));
        }


    }
}
