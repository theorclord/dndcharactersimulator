using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DndCharacterSimulator.Models
{
    public class Building
    {
        public string Name { get; set; }
        public int BaseProduction { get; set; }
        public List<Item> ProductionItems { get; set; }
        public List<Item> ConstructionQueue {  get; set; }
    }
}
