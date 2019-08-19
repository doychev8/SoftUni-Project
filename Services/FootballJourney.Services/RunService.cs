using FootballJourney.Data;
using FootballJourney.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballJourney.Services
{
    public class RunService : IRunService
    {
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
    }
}
