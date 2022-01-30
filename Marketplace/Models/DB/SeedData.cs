using System.Collections.ObjectModel;
using System.Linq;
using Forum_MVC.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Marketplace.Models.DB
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<Context>();
            if (context.Database.GetPendingMigrations().Any()) context.Database.Migrate();

            //Admin password: Passw0rd%
            if (!context.Users.Any())
            {
                var categories = new Collection<Category>
                {
                    new() { CategoryName = "Transport" },
                    new() { CategoryName = "Clothes" },
                    new() { CategoryName = "Real estate" },
                    new() { CategoryName = "Appliances" },
                    new() { CategoryName = "Smartphones" },
                    new() { CategoryName = "Computers" },
                    new() { CategoryName = "Other electronic" }
                };
                context.Categories.AddRange(categories);
                var users = new Collection<User>
                {
                    new()
                    {
                        Email = "admin@gmail.com",
                        Username = "Admin",
                        IsActivated = true,
                        PhoneNumber = "0963456756",
                        PasswordHash = PasswordHash.CreateHash("Passw0rd%")
                    }
                };
                context.Users.AddRange(users);
                context.SaveChanges();
            }
        }
    }
}