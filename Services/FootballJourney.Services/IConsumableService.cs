using FootballJourney.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballJourney.Services
{
    public interface IConsumableService
    {
        List<Consumable> GetConsumables();

        Consumable GetConsumable(string consumableId);
    }
}
