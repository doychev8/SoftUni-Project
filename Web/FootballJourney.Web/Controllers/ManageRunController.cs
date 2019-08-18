using FootballJourney.Data.Models;
using FootballJourney.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FootballJourney.Web.Controllers
{
    public class ManageRunController : BaseController
    {
        private readonly IRunService runService;
        private readonly IUserService userService;

        public ManageRunController(IRunService runService, IUserService userService)
        {
            this.runService = runService;
            this.userService = userService;
        }

        [Authorize]
        public IActionResult ListAllRuns()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = this.userService.GetUser(userId);

            List<Data.Models.Run> userRuns = this.runService.GetRunsByUser(userId);

            return this.View(userRuns);
        }

        public IActionResult ActiveRunError()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult AbbandonRun()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = this.userService.GetUser(userId);
            var currentRun = user.CurrentRun;

            this.runService.AbandonRun(userId, currentRun);

            return this.Redirect("/StartRun/Start");
        }

        public IActionResult ShowCurrentRun()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = this.userService.GetUser(userId);

            var run = this.runService.GetCurrentRun(user);

            return this.View(run);
        }

        
        public IActionResult GetNextOpponent()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = this.userService.GetUser(userId);
            var currentRun = this.runService.GetCurrentRun(user);

            var opponent = this.runService.GetOpponent(currentRun.Stage);


            return this.View(opponent);
        }
    }
}
