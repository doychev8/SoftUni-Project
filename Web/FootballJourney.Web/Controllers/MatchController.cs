using FootballJourney.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FootballJourney.Web.Controllers
{
    public class MatchController : BaseController
    {
        private readonly IMatchService matchService;
        private readonly IUserService userService;

        public MatchController(IMatchService matchService, IUserService userService)
        {
            this.matchService = matchService;
            this.userService = userService;
        }

        public IActionResult Play()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = this.userService.GetUser(userId);

            var run = user.CurrentRun;

            var result = this.matchService.CalculateResult(run);

            if (result == true)
            {
                return this.View("Victory", run);
            }

            return this.View("Defeat", run);
        }
    }
}
