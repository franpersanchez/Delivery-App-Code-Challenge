using DB.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class DbInitializer
    {
        /// <summary>
        /// Initializes the specified service provider.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            //DI
            var dbContextFactory = serviceProvider.GetRequiredService<IDbContextFactory<DeliveryAppContext>>();

            //Migrations
            var isConnected = false;
            using var applicationDbContext = await dbContextFactory.CreateDbContextAsync();

            while (isConnected == false)
            {
                try
                {
                    applicationDbContext.Database.Migrate();
                    isConnected = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.ToString());
                }
                Thread.Sleep(1_000);
            }
        }
    }
}