using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.identity;

namespace Talabat.Repository.Identity
{
    public static class AppIdentityDbContextSeed
    {

        public async static Task SeedIdentityAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "NouraGamil",
                    Email = "ngamil025g@gmail.com",
                    UserName = "ngamil025g",
                    PhoneNumber = "01207552620"
                };
                var result = await userManager.CreateAsync(user, "Admin123");


                if (!result.Succeeded)
                {
                    // Log the error details
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"Error: {error.Description}");
                    }
                }
            }
        }
    }
}
