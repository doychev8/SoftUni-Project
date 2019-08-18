using FootballJourney.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballJourney.Data.Models
{
    public class Consumable
    {
        public string Id { get; set; }

        public int Tier { get; set; }

        public int Points { get; set; }

        public ConsumableType Type { get; set; }
    }
}
