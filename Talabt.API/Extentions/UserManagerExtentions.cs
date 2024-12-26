using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.Core.Entities.identity;

namespace Talabt.API.Extentions
{
    public  static class UserManagerExtentions
    {
        public static  async Task<AppUser?>FindUserWithAddressAsync(this UserManager<AppUser> userManager,ClaimsPrincipal User )
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user= await userManager.Users.Include(U=>U.Addresses).FirstOrDefaultAsync();
            return user ;

        }
    }
}
