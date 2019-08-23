using FootballJourney.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballJourney.Data.Models
{
    public class Consumable
    {
        public string Id { get; set; }

        public string Name { get; set; }
        
        public int Points { get; set; }

        public ConsumableType Type { get; set; }

        public Position Position { get; set; }
    }
}
