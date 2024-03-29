﻿using FootballJourney.Services;
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
        private readonly IRunService runService;

        public MatchController(IMatchService matchService, IUserService userService, IRunService runService)
        {
            this.matchService = matchService;
            this.userService = userService;
            this.runService = runService;
        }

        public IActionResult Play()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = this.userService.GetUser(userId);

            var run = user.CurrentRun;

            var result = this.matchService.CalculateResult(run);

            this.ViewBag.userGoals = result["userGoals"];
            this.ViewBag.opponentGoals = result["opponentGoals"];

            if (result["isWinner"] == 1)
            {
                if (run.Stage == 5)
                {
                    this.ViewBag.OpponentName = run.CurrentOpponent.Name;
                    user.AllTimeOpponentsBeaten++;
                    user.AllTimeWins++;
                    this.runService.AbandonRun(userId, run);
                    return this.View("LastVictory", run);
                }

                user.AllTimeOpponentsBeaten++;
                this.ViewBag.Opponent = run.CurrentOpponent.Name;
                this.runService.GoToNextStage(run);
                return this.View("Victory", run);
            }

            this.runService.AbandonRun(userId, run);
            return this.View("Defeat", run);
        }
    }
}
