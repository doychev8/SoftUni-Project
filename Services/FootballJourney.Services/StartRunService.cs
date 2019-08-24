using System;
using System.Collections.Generic;
using System.Linq;

using FootballJourney.Data;
using FootballJourney.Data.Models;

namespace FootballJourney.Services
{
    public class StartRunService : IStartRunService
    {
        private static Random rng = new Random();
        private readonly ApplicationDbContext context;


        public StartRunService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void ApplyRunToUser(string userId, string runId)
        {
            var user = this.context.Users.Where(x => x.Id == userId).FirstOrDefault();
            var run = this.context.Runs.FirstOrDefault(x => x.Id == runId);

            user.HasActiveRun = true;
            user.CurrentRun = run;

            this.context.SaveChanges();
        }

        public Team CreateTeam(string name, List<Player> players)
        {
            var team = new Team()
            {
                AttackPoints = players.Where(x => x.Position == FootballJourney.Data.Models.Enums.Position.Attacker)
                .Sum(x => x.Points),

                DefencePoints = players.Where(x => x.Position == FootballJourney.Data.Models.Enums.Position.Defender
                || x.Position == FootballJourney.Data.Models.Enums.Position.Goalkeeper)
                .Sum(x => x.Points),

                MidfieldPoints = players.Where(x => x.Position == FootballJourney.Data.Models.Enums.Position.Midfielder)
                .Sum(x => x.Points),

                Name = name,
                Players = players,
                IsOpponentTeam = false,
            };

            this.context.Teams.Add(team);

            this.context.SaveChanges();

            return team;
        }

        public List<Player> GenerateTeam()
        {
            var players = new List<Player>();

            var keeper = this.GetGoalkeeper();
            var defenders = this.GetDefenders();
            var midfielders = this.GetMidfielders();
            var attackers = this.GetAttackers();

            players.Add(keeper);
            players.AddRange(defenders);
            players.AddRange(midfielders);
            players.AddRange(attackers);

            return players;

        }

        

        public Run StartRun(string userId, string teamId, Team team)
        {
            var run = new Run()
            {
                User = this.context.Users.FirstOrDefault(x => x.Id == userId),
                Team = this.context.Teams.FirstOrDefault(x => x.Id == teamId),
                IsEnded = false,
                Money = 100,
                Stage = 1,
            };

            this.context.Runs.Add(run);
            this.context.SaveChanges();

            return run;
        }

        private List<Player> GetAttackers()
        {
            var attackers = new List<Player>();
            var attackersFromDb = this.context.Players.Where(x => x.Position == FootballJourney.Data.Models.Enums.Position.Attacker && x.IsOpponentPlayer == false).ToList();

            attackersFromDb = this.Shuffle(attackersFromDb);

            attackers = attackersFromDb.Take(2).ToList();

            return attackers;
        }

        private List<Player> GetDefenders()
        {
            var defenders = new List<Player>();
            var defendersFromDb = this.context.Players.Where(x => x.Position == FootballJourney.Data.Models.Enums.Position.Defender && x.IsOpponentPlayer == false).ToList();

            defendersFromDb = this.Shuffle(defendersFromDb);

            defenders = defendersFromDb.Take(4).ToList();

            return defenders;
        }

        private Player GetGoalkeeper()
        {
            var keepers = this.context.Players.Where(x => x.Position == FootballJourney.Data.Models.Enums.Position.Goalkeeper && x.IsOpponentPlayer == false).ToList();
            var random = new Random();

            var index = random.Next(0, keepers.Count - 1);
            var keeper = keepers[index];

            return keeper;
        }

        private List<Player> GetMidfielders()
        {
            var midfielders = new List<Player>();
            var midfieldersFromDb = this.context.Players.Where(x => x.Position == FootballJourney.Data.Models.Enums.Position.Midfielder && x.IsOpponentPlayer == false).ToList();

            midfieldersFromDb = this.Shuffle(midfieldersFromDb);

            midfielders = midfieldersFromDb.Take(4).ToList();

            return midfielders;
        }

        private List<T> Shuffle<T>(List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            return list;
        }
    }
}
