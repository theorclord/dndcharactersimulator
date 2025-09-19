using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DndCharacterSimulator.Models.Race;

namespace DndCharacterSimulator.Models
{
    public class PopulationGroup
    {
        public int Individuals { get; set; }

        public StatLine PopulationStatLine { get; set; }

        public RaceType RaceType { get; set; }

        public PopulationGroup(int individuals, StatLine populationStatLine, RaceType raceType)
        {
            Individuals = individuals;
            PopulationStatLine = populationStatLine;
            RaceType = raceType;
        }

    }
}
