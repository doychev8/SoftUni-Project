using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FootballJourney.Data.Models
{
    public class Run
    {
        public Run()
        {
            this.BeatenOpponents = new List<Team>();
        }

        public string Id { get; set; }

        public virtual ApplicationUser User { get; set; }
        public string UserId { get; set; }

        public virtual Team Team { get; set; }
        public string TeamId { get; set; }

        public virtual Team CurrentOpponent { get; set; }
        public string CurrentOpponentId { get; set; }

        public int Stage { get; set; }

        public bool IsEnded { get; set; }

        public bool HasMadeTransfer { get; set; }

        public bool HasBoughtConsumable { get; set; }

        public DateTime StartedOn { get; set; }
        public virtual ICollection<Team> BeatenOpponents { get; set; }
    }
}
