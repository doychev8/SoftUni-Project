using FootballJourney.Data;
using FootballJourney.Data.Models;
using FootballJourney.Services;
using FootballJourney.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FootballJourney.Web.Controllers
{
    public class StartRunController : BaseController
    {
        private readonly IStartRunService service;
        private readonly IUserService userService;

        public StartRunController(IStartRunService service, IUserService userService)
        {
            this.service = service;
            this.userService = userService;
        }


        [Authorize]
        public IActionResult Start()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Start(CreateTeamViewModel model)
        {
            this.ViewData["Team Name"] = model.TeamName;

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUser = this.userService.GetUser(userId);

            if (currentUser.HasActiveRun == true)
            {
                return this.Redirect("/ManageRun/ActiveRunError");
            }

            var players = this.service.GenerateTeam();
            var team = this.service.CreateTeam(model.TeamName, players);
            var run = this.service.StartRun(userId, team.Id, team);

            this.service.ApplyRunToUser(userId, run.Id);

            return this.View("Commence", team);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Commence(Team team)
        {
            return this.View(team);
        }

    }
}
