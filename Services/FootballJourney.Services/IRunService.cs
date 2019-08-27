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

        Team GetOpponent(Run run);

        void GoToNextStage(Run run);

        void MakeTransfer(Run run, Player player);

        void ApplyConsumable(Consumable consumable, Run run);

        Run GetRunById(string id);
    }
}
