using DndCharacterSimulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            return city;
        }
    }
}
