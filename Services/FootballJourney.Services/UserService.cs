using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FootballJourney.Data;
using FootballJourney.Data.Models;
using Microsoft.AspNetCore.Http;

namespace FootballJourney.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext db;
        private readonly IHttpContextAccessor contextAccessor;

        public UserService(ApplicationDbContext db, IHttpContextAccessor contextAccessor)
        {
            this.db = db;
            this.contextAccessor = contextAccessor;
        }

        public int GetAllTimeOpponentsBeaten(string username)
        {
            var user = this.db.Users.Where(x => x.UserName == username).FirstOrDefault();
            
            var allTimeOpponentsBeaten = user.AllTimeOpponentsBeaten;
            return allTimeOpponentsBeaten;
        }

        public int GetAllTimeWins(string username)
        {
            var user = this.db.Users.Where(x => x.UserName == username).FirstOrDefault();


            var allTimeWins = user.AllTimeWins;
            return allTimeWins;
        }

        public ApplicationUser GetUser(string userId)
        {
            var user = this.db.Users.FirstOrDefault(x => x.Id == userId);

            return user;
        }

        
    }
}
