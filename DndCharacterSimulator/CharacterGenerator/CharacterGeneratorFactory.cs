using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DndCharacterSimulator.CharacterGenerator
{
    internal class CharacterGeneratorFactory
    {
        /// <summary>
        /// Generates a statline based on a probability distribution.
        /// </summary>
        /// <param name="statSum">The sum of all stat points</param>
        /// <param name="probabilityDistribution">The probability of each stat</param>
        /// <param name="minimumStat">The minimum stat which each stat must have</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<int> GenerateStatLine(int statSum, List<int> probabilityDistribution, int minimumStat)
        {
            var minimunStatSum = minimumStat * 6;
            // check that the statsum is larger than the minimum array
            if (minimunStatSum > statSum)
            {
                throw new Exception("Unable to generate stat line. Minimum stat sum is larger than allocated stats");
            }

            // Create the stat line and set minium stat
            var statLine = new List<int>(6);
            for (var i = 0; i < 6; i++) { statLine.Add(minimumStat); }


            // populate the rest of the stats
            var probabilitySum = probabilityDistribution.Sum();
            var statsToAssign = statSum - minimunStatSum;

            // TODO add code for checking for maximum stat limit. 
            // EG, if 18 is the max, do reroll with the stat over 18 probability being set to 0

            var random = new Random();
            for (var i = 0; i < statsToAssign; i++)
            {
                var statProbablilty = random.Next(probabilitySum)+1;
                var currentProbabilityThreshold = 0;
                for (var j = 0; j < statLine.Count; j++)
                {
                    currentProbabilityThreshold += probabilityDistribution[j];
                    if (statProbablilty <= currentProbabilityThreshold)
                    {
                        statLine[j] += 1;
                        break;
                    }
                }
            }

            return statLine;
        }

        public List<int> GenerateProbabilityDistributionStandardSummation(List<int> statLine1, List<int> statLine2)
        {
            if (statLine1.Count != statLine2.Count)
            {
                throw new Exception("The statline lenghts does not match");
            }

            var probabilityDistribution = new List<int>();
            for (var i = 0; i < statLine1.Count; i++)
            {
                var parent1Summation = statLine1[i] * (statLine1[i] + 1);
                var parent2Summation = statLine2[i] * (statLine2[i] + 1);
                var summationValue = parent1Summation + parent2Summation;
                probabilityDistribution.Add(summationValue);
            }

            return probabilityDistribution;
        }

        public List<int> GenerateProbabilityDistributionExponential(List<int> statLine1, List<int> statLine2)
        {
            if (statLine1.Count != statLine2.Count)
            {
                throw new Exception("The statline lenghts does not match");
            }

            var probabilityDistribution = new List<int>();
            for (var i = 0; i < statLine1.Count; i++)
            {
                var parent1Exponential = (int) Math.Pow(2, statLine1[i]);
                var parent2Exponential = (int) Math.Pow(2, statLine2[i]);
                var summationValue = parent1Exponential + parent2Exponential;
                probabilityDistribution.Add(summationValue);
            }

            return probabilityDistribution;
        }

        public List<int> GenerateStatLineChromosone(List<int> statLine1, List<int> statLine2, int statSum)
        {
            var childStatLine = new List<int>();

            // select a random stat from each stat line to create the child statline.
            // normalize for stat sum
            var random = new Random();

            for (var i = 0; i < 6; i++)
            {
                var chance = random.Next(100)+1;
                if(chance <= 50)
                {
                    childStatLine.Add(statLine1[i]);
                } else
                {
                    childStatLine.Add(statLine2[i]);
                }
                
            }

            return childStatLine;
        }
    }
}
