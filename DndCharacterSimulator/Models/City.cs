using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DndCharacterSimulator.Models
{
    public class City
    {
        public List<PopulationGroup> PopulationGroups {  get; set; }
        public List<Building> Buildings { get; set; }
        public int BaseProduction { get; set; }

        public int Population { get { return PopulationGroups.Sum(x => x.Individuals); } }

        public City() 
        {
            PopulationGroups = new List<PopulationGroup>();
            Buildings = new List<Building>();
        }
    }
}
