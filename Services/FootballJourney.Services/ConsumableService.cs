using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FootballJourney.Data;
using FootballJourney.Data.Models;

namespace FootballJourney.Services
{
    public class ConsumableService : IConsumableService
    {
        private static Random rng = new Random();
        private readonly ApplicationDbContext db;

        public ConsumableService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public Consumable GetConsumable(string consumableId)
        {
            var consumable = this.db.Consumables.FirstOrDefault(x => x.Id == consumableId);

            return consumable;
        }

        public List<Consumable> GetConsumables()
        {
            var consumables = new List<Consumable>();
            var consumablesFromDb = this.db.Consumables.ToList();

            consumablesFromDb = this.Shuffle(consumablesFromDb);

            consumables = consumablesFromDb.Take(3).ToList();

            return consumables;
        }


        private List<T> Shuffle<T>(List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            return list;
        }
    }
}
