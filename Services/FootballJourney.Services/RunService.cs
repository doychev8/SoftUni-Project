using FootballJourney.Data;
using FootballJourney.Data.Models;
using FootballJourney.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballJourney.Services
{
    public class RunService : IRunService
    {
        private static Random rng = new Random();
        private readonly ApplicationDbContext db;

        public RunService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void AbandonRun(string userId, Run run)
        {
            var user = this.db.Users.FirstOrDefault(x => x.Id == userId);
            user.Runs.Add(run);
            run.IsEnded = true;

            user.CurrentRun = null;
            user.HasActiveRun = false;

            foreach (var player in run.Team.Players)
            {
                player.Points = player.InitialPoints;
            }

            this.db.SaveChanges();

        }

        public void ApplyConsumable(Consumable consumable, Run run)
        {
            var consumableType = consumable.Type;
            var consumablePosition = consumable.Position;



            switch (consumableType)
            {
                case ConsumableType.Team:
                    switch (consumablePosition)
                    {
                        case Position.Defender:
                            foreach (var defender in run.Team.Players.Where(x => x.Position == Position.Defender))
                            {
                                defender.Points += consumable.Points;
                            }
                            break;
                        case Position.Midfielder:
                            foreach (var midfielder in run.Team.Players.Where(x => x.Position == Position.Midfielder))
                            {
                                midfielder.Points += consumable.Points;
                            }
                            break;
                        case Position.Attacker:
                            foreach (var attacker in run.Team.Players.Where(x => x.Position == Position.Attacker))
                            {
                                attacker.Points += consumable.Points;
                            }
                            break;
                    }
                    break;

                case ConsumableType.Individual:
                    switch (consumable.Position)
                    {

                        case Position.Goalkeeper:
                            var keeper = run.Team.Players.Where(x => x.Position == Position.Goalkeeper).FirstOrDefault();
                            keeper.Points += consumable.Points;
                            break;
                        case Position.Defender:
                            var defenders = run.Team.Players.Where(x => x.Position == Position.Defender).ToList();
                            var defenderIndex = rng.Next(0, defenders.Count - 1);
                            defenders[defenderIndex].Points += consumable.Points;
                            break;
                        case Position.Midfielder:
                            var midfielders = run.Team.Players.Where(x => x.Position == Position.Midfielder).ToList();
                            var midfielderIndex = rng.Next(0, midfielders.Count - 1);
                            midfielders[midfielderIndex].Points += consumable.Points;
                            break;
                        case Position.Attacker:
                            var attackers = run.Team.Players.Where(x => x.Position == Position.Attacker).ToList();
                            var attackerIndex = rng.Next(0, attackers.Count - 1);
                            attackers[attackerIndex].Points += consumable.Points;
                            break;
                    }
                    break;
                
            }

            foreach (var player in run.Team.Players)
            {
                if (player.Points > 99)
                {
                    player.Points = 99;
                }
            }
            
            run.HasBoughtConsumable = true;
            this.db.SaveChanges();
        }

        public Run GetCurrentRun(ApplicationUser user)
        {
            var run = user.CurrentRun;
            return run;
        }

        public Team GetOpponent(Run run)
        {
            var opponents = this.db.Teams.Where(x => x.IsOpponentTeam == true).ToList();

            var opponent = this.AssignOpponent(run.Stage, opponents);
            run.CurrentOpponent = opponent;

            this.db.SaveChanges();

            return opponent;

        }

        public List<Run> GetRunsByUser(string userId)
        {
            
            var user = this.db.Users.FirstOrDefault(x => x.Id == userId);
            var runs = user.Runs.Where(x => x.IsEnded == true).ToList();


            return runs;
        }

        public void GoToNextStage(Run run)
        {
            run.Stage++;
            run.CurrentOpponent = null;
            run.HasBoughtConsumable = false;
            run.HasMadeTransfer = false;
            this.db.SaveChanges();
        }

        public void MakeTransfer(Run run, Player player)
        {
            var playerToTransfer = this.GetPlayerForTransfer(player.Points, player.Position);

            player.Points = player.InitialPoints;

            run.Team.Players.Remove(player);
            run.Team.Players.Add(playerToTransfer);

            run.HasMadeTransfer = true;

            this.db.SaveChanges();
        }

        private Team AssignOpponent(int stage, List<Team> opponents)
        {

            var opponent = new Team();
            var random = new Random();
            int index = 0;

            switch (stage)
            {
                case 1: opponents = opponents.Where(x => x.Tier == 5).ToList();
                    index = random.Next(0, opponents.Count - 1);
                    opponent = opponents[index];
                    break;

                case 2:
                    opponents = opponents.Where(x => x.Tier == 5 || x.Tier == 4).ToList();
                    index = random.Next(0, opponents.Count - 1);
                    opponent = opponents[index];
                    break;
                case 3:
                    opponents = opponents.Where(x => x.Tier == 4 || x.Tier == 3).ToList();
                    index = random.Next(0, opponents.Count - 1);
                    opponent = opponents[index];
                    break;
                case 4:
                    opponents = opponents.Where(x => x.Tier == 3 || x.Tier == 2).ToList();
                    index = random.Next(0, opponents.Count - 1);
                    opponent = opponents[index];
                    break;
                case 5:
                    opponents = opponents.Where(x => x.Tier == 1).ToList();
                    index = random.Next(0, opponents.Count - 1);
                    opponent = opponents[index];
                    break;
            }

            return opponent;
        }

        private Player GetPlayerForTransfer(int points, Position position)
        {
            var eligiblePlayers = this.db.Players.Where(x => x.Position == position
                && x.Points > points).ToList();

            if (eligiblePlayers.Count == 0)
            {
                eligiblePlayers = this.db.Players.Where(x => x.Position == position).ToList();
            }

            var random = new Random();
            var index = random.Next(0, eligiblePlayers.Count - 1);

            var player = eligiblePlayers[index];

            return player;
        }

        
    }
}
