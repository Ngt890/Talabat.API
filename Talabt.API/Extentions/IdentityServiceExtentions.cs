using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Talabat.Core.Entities.identity;
using Talabat.Core.Services;
using Talabat.Service;
using Talabat.Repository.Identity;

namespace Talabt.API.Extentions
{ 
    public  static class IdentityServiceExtentions
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection Services,IConfiguration _configuration)
        {
            Services.AddScoped<ITokenProvider, TokenProvider>();    
            Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;             // Require a digit
                options.Password.RequiredLength = 6;               // Minimum password length
                options.Password.RequireLowercase = true;         // Require lowercase letter
                options.Password.RequireUppercase = true;         // Require uppercase letter
                options.Password.RequireNonAlphanumeric = false;  // Optional, disable special characters
                options.Password.RequiredUniqueChars = 1;         // Minimum unique characters
            })
                    .AddEntityFrameworkStores<AppIdentityDbContext>();

            Services.AddAuthentication(options=>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;
            })

                .AddJwtBearer(options=>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer=true,
                        ValidIssuer= _configuration["JWT:Issuer"],
                        ValidateAudience=true,
                        ValidAudience = _configuration["JWT:Audience"],
                        ValidateLifetime=true,
                        ValidateIssuerSigningKey=true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]))


                    };
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        // Access claims to validate email or other custom logic
                        var emailClaim = context.Principal?.FindFirst(ClaimTypes.Email)?.Value;

                        if (string.IsNullOrEmpty(emailClaim))
                        {
                            // If email is not found in claims, reject the request
                            context.Fail("Email claim not found in token.");
                        }

                      

                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        
                        return Task.CompletedTask;
                    }
                };
                });





            return Services;    

        }
    }
}
