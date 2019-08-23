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
        private readonly IPlayerService playerService;
        private readonly IConsumableService consumableService;

        public ManageRunController(IRunService runService, IUserService userService, IPlayerService playerService, IConsumableService consumableService)
        {
            this.runService = runService;
            this.userService = userService;
            this.playerService = playerService;
            this.consumableService = consumableService;
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

            if (run == null)
            {
                return this.View("NoActiveRunError");
            }


            return this.View(run);
        }

        public IActionResult TransferPlayer()
        {
            var playerId = this.Request.Form["players"].ToString();
            var player = this.playerService.GetPlayer(playerId);

            var user = this.GetUser();

            this.runService.MakeTransfer(user.CurrentRun, player);

            return this.View();
        }

        public IActionResult BuyConsumable()
        {
            var consumables = this.consumableService.GetConsumables();

            return this.View(consumables);
        }

        public IActionResult ApplyConsumable()
        {
            var consumableId = this.Request.Form["ApplyingConsumable"].ToString();
            var consumable = this.consumableService.GetConsumable(consumableId);

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = this.userService.GetUser(userId);

            var run = this.runService.GetCurrentRun(user);

            this.runService.ApplyConsumable(consumable, run.Team);


            return this.View();
        }

        public IActionResult MakeTransfer()
        {

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = this.userService.GetUser(userId);

            var run = this.runService.GetCurrentRun(user);
            var currentTeam = run.Team.Players.ToList();

            return this.View(currentTeam);
        }
        
        public IActionResult GetNextOpponent()
        {

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = this.userService.GetUser(userId);
            var currentRun = this.runService.GetCurrentRun(user);

            if (currentRun.CurrentOpponent != null)
            {
                return this.View(currentRun);
            }

            this.runService.GetOpponent(currentRun);

            return this.View(currentRun);
        }

        private ApplicationUser GetUser()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = this.userService.GetUser(userId);

            return user;
        }
    }
}
