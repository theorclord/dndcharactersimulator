using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DndCharacterSimulator.Models
{
    internal class City
    {
        public List<PopulationGroup> PopulationGroups {  get; set; }
        public List<Building> Buildings { get; set; }
        public int BaseProduction { get; set; }
    }
}
