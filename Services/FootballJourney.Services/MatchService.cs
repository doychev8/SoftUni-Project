using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FootballJourney.Data;
using FootballJourney.Data.Models;
using FootballJourney.Data.Models.Enums;

namespace FootballJourney.Services
{
    public class MatchService : IMatchService
    {
        private readonly ApplicationDbContext db;

        public MatchService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public bool CalculateResult(Run run)
        {
            var userTeam = run.Team;
            var opponent = run.CurrentOpponent;

            var userPoints = 0;
            var opponentPoints = 0;


            var userTeamAttackingPoints = this.GetPoints(userTeam, Position.Attacker);
            var userTeamDefensePoints = this.GetPoints(userTeam, Position.Defender);
            userTeamDefensePoints += this.GetPoints(userTeam, Position.Goalkeeper);
            var userTeamMidfieldPoints = this.GetPoints(userTeam, Position.Midfielder);


            var opponentTeamAttackingPoints = this.GetPoints(opponent, Position.Attacker);
            var opponentTeamDefensePoints = this.GetPoints(opponent, Position.Defender);
            opponentTeamDefensePoints += this.GetPoints(opponent, Position.Goalkeeper);
            var opponentTeamMidfieldPoints = this.GetPoints(opponent, Position.Midfielder);

            if (userTeamAttackingPoints >= (opponentTeamDefensePoints / 2.50))
            {
                userPoints += (int)(userTeamAttackingPoints - (opponentTeamDefensePoints / 2.50));
            }
            else
            {
                opponentPoints += (int)((opponentTeamDefensePoints / 2.50) - userTeamAttackingPoints);
            }

            if (userTeamDefensePoints / 2.50 >= opponentTeamAttackingPoints)
            {
                userPoints += (int)((userTeamDefensePoints / 2.50) - opponentTeamAttackingPoints);
            }
            else
            {
                opponentPoints += (int)(opponentTeamAttackingPoints - (userTeamDefensePoints / 2.50));
            }

            if (userTeamMidfieldPoints >= opponentTeamMidfieldPoints)
            {
                userPoints += userTeamMidfieldPoints - opponentTeamMidfieldPoints;
            }
            else
            {
                opponentPoints += opponentTeamMidfieldPoints - userTeamMidfieldPoints;
            }


            if (userPoints >= opponentPoints)
            {
                return true;
            }

            return false;
        }



        private int GetPoints(Team team, Position position)
        {
            var points = team.Players.Where(x => x.Position == position).Sum(x => x.Points);

            return points;
        }
    }
}
