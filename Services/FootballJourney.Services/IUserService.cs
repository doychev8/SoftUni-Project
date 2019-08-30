using FootballJourney.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballJourney.Services
{
    public interface IUserService
    {
        ApplicationUser GetUser(string userId);

        int GetAllTimeOpponentsBeaten(string username);
        int GetAllTimeWins(string username);
    }
}
