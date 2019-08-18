using FootballJourney.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballJourney.Services
{
    public interface IRunService
    {
        void AbandonRun(string userId, Run run);

        List<Run> GetRunsByUser(string userId);

        Run GetCurrentRun(ApplicationUser user);

        Team GetOpponent(int stage);
    }
}
