﻿namespace FootballJourney.Web
{

    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;


    public static class Program
    {
        public static void Main(string[] args)
        {
            // TODO: Try listing the team in transfer and consumable page

            CreateWebHostBuilder(args).Build().Run();


        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
    }
}
