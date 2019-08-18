using FootballJourney.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballJourney.Data.Models
{
    public class Player
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Tier { get; set; }

        public Position Position { get; set; }

        public int Points { get; set; }
    }
}
