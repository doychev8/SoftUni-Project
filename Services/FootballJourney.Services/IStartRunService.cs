namespace FootballJourney.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using FootballJourney.Data.Models;

    public interface IStartRunService
    {
        List<Player> GenerateTeam();

        Team CreateTeam(string name, List<Player> players);

        Run StartRun(string userId, string userTeamId, Team team);

        void ApplyRunToUser(string userId, string runId);
    }
}
