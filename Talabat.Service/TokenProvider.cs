using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.identity;
using Talabat.Core.Services;

namespace Talabat.Service
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IConfiguration _configuration;

        public TokenProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager)
        {//Payload  --Private[name,mail]


            var Claims = new List<Claim>
            {
              new Claim(ClaimTypes.Name, user.UserName),      // UserName
               new Claim(ClaimTypes.Email, user.Email)        // Email claim

                };


            

            var Roles = await userManager.GetRolesAsync(user);

            foreach (var Role in Roles)
            { Claims.Add(new Claim(ClaimTypes.Role, Role)); }

            //CreatingKey

            var AuthKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            //CreateObjToken
            var Token = new JwtSecurityToken(
                
               issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: Claims,
                expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:ExpirationDate"])),
                signingCredentials:new SigningCredentials( AuthKey, SecurityAlgorithms.HmacSha256Signature)


                );


                
               
            return new JwtSecurityTokenHandler().WriteToken(Token);



        }
    } 
}
