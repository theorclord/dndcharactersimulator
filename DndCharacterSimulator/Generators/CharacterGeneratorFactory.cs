using DndCharacterSimulator.Models;

namespace DndCharacterSimulator.Generators
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
        public StatLine GenerateStatLine(int statSum, List<int> probabilityDistribution, int minimumStat, int maximumStat)
        {
            var workingProbabilityDistribution = new List<int>(probabilityDistribution);
            var minimunStatSum = minimumStat * 6;
            // check that the statsum is larger than the minimum array
            if (minimunStatSum > statSum)
            {
                throw new Exception("Unable to generate stat line. Minimum stat sum is larger than allocated stats");
            }

            // Create the stat line and set minium stat
            var statLine = new int[6];
            for (var i = 0; i < 6; i++) { statLine[i] = minimumStat; }


            // populate the rest of the stats
            var probabilitySum = workingProbabilityDistribution.Sum();
            var statsToAssign = statSum - minimunStatSum;

            var random = new Random();
            for (var i = 0; i < statsToAssign; i++)
            {
                var statProbablilty = random.Next(probabilitySum)+1;
                var currentProbabilityThreshold = 0;
                for (var j = 0; j < statLine.Length; j++)
                {
                    currentProbabilityThreshold += workingProbabilityDistribution[j];
                    if (statProbablilty <= currentProbabilityThreshold)
                    {
                        statLine[j] += 1;
                        // If maximum stat is reached, exclude stat from assignment
                        if (statLine[j] == maximumStat)
                        {
                            probabilitySum -= workingProbabilityDistribution[j];
                            workingProbabilityDistribution[j] = 0;
                        }
                        break;
                    }
                }
            }

            return new StatLine(statLine);
        }

        /// <summary>
        /// Using two statlines, generate a distribution, where the weight of each individual stat influence the outcome via polynomial strength
        /// </summary>
        /// <param name="statLine1"></param>
        /// <param name="statLine2"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<int> GenerateProbabilityDistributionPolynomial(int[] statLine1, int[] statLine2)
        {
            if (statLine1.Length != statLine2.Length)
            {
                throw new Exception("The statline lenghts does not match");
            }

            var probabilityDistribution = new List<int>();
            for (var i = 0; i < statLine1.Length; i++)
            {
                var parent1Summation = Math.Pow(statLine1[i], 3);
                var parent2Summation = Math.Pow(statLine2[i], 3);
                var summationValue = (int)parent1Summation + (int)parent2Summation;
                probabilityDistribution.Add(summationValue);
            }

            return probabilityDistribution;
        }

        /// <summary>
        /// TODO still not tested. Use two statlines to create a new one
        /// </summary>
        /// <param name="statLine1"></param>
        /// <param name="statLine2"></param>
        /// <param name="statSum"></param>
        /// <returns></returns>
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
