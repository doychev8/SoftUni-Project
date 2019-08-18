using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace FootballJourney.Data.Models
{
    public class Team
    {
        public Team()
        {
            this.Players = new List<Player>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public int? Tier { get; set; }

        public virtual ICollection<Player> Players { get; set; }

        public int AttackPoints { get; set; }

        public int MidfieldPoints { get; set; }

        public int DefencePoints { get; set; }

        public bool IsOpponentTeam { get; set; }


    }
}
