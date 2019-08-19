using FootballJourney.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballJourney.Services
{
    public interface IMatchService
    {

        bool CalculateResult(Run run);
    }
}
