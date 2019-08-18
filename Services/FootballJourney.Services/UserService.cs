using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FootballJourney.Data;
using FootballJourney.Data.Models;

namespace FootballJourney.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext db;

        public UserService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public ApplicationUser GetUser(string userId)
        {
            var user = this.db.Users.FirstOrDefault(x => x.Id == userId);

            return user;
        }

        
    }
}
