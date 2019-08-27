using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FootballJourney.Web.ViewModels
{
    public class CreateTeamViewModel
    {
        [Required]
        [Display(Name = "Team Name")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string TeamName { get; set; }
    }
}
