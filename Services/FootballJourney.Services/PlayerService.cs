using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FootballJourney.Data;
using FootballJourney.Data.Models;

namespace FootballJourney.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly ApplicationDbContext db;

        public PlayerService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public Player GetPlayer(string playerId)
        {
            var player = this.db.Players.FirstOrDefault(x => x.Id == playerId);

            return player;
        }
    }
}
