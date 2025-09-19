using DndCharacterSimulator.Models;

namespace DndCharacterSimulator.Generators
{
    public class CityGeneratorFactory
    {
        public static City GenerateCity(int populationCount, Dictionary<Race.RaceType,int> raceDistribution)
        {
            // Create population groups
            // Calculate the distribution to get individuals in each group
            var distributionSum = 0;
            foreach (var race in raceDistribution) 
            {
                distributionSum += race.Value;
            }

            var populationFactor = populationCount / distributionSum;

            var populationGroups = new List<PopulationGroup>();
            foreach (var race in raceDistribution) 
            {
                // TODO generate stat line
                var popStatLine = new StatLine(new int[] { 15, 14, 13, 12, 10, 8 });
                var individuals = populationFactor * race.Value;
                var popGroup = new PopulationGroup(individuals, popStatLine, race.Key);
                
                populationGroups.Add(popGroup);
            }

            var city = new City();
            city.PopulationGroups.AddRange(populationGroups);
            
            // As the distribution might not result in complete integer solution
            // Add remaining individuals to the population groups, to achieve saturation
            var currentCityPopulation = city.Population;
            if (currentCityPopulation < populationCount)
            {
                var difference = populationCount - currentCityPopulation;
                var random = new Random();
                for(var i = 0; i< difference; i++)
                {
                    // Allocate based on current number of individuals
                    var distAllocation = random.Next(city.Population) + 1;

                    var currentProbabilityThreshold = 0;
                    for (var j = 0; j < city.PopulationGroups.Count; j++)
                    {
                        currentProbabilityThreshold += city.PopulationGroups[j].Individuals;
                        if (distAllocation <= currentProbabilityThreshold)
                        {
                            city.PopulationGroups[j].Individuals += 1;
                            break;
                        }
                    }
                }
            }

            return city;
        }
    }
}
