
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.Core.Entities.identity;
using Talabat.Core.Repositories.Contract;
using Talabt.API.Errors;
using Talabt.API.Extentions;
using Talabt.API.Helpers;
using Talabt.API.MiddleWares;
using Talabat.Repository.Data;
using Talabat.Repository;
using Talabat.Repository.Identity;
using Talabat.Repository.Data.DataSeeding;

namespace Talabt.API
{
    public class Program

    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            #region  Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddDbContext<StoreContext>(options =>

           {
               options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

           });
            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("identity"));
            });
            builder.Services.AddSingleton<IConnectionMultiplexer>(options =>
            {
                var Connection = builder.Configuration.GetConnectionString("RedisConnection");
                return ConnectionMultiplexer.Connect(Connection);   


           } );
           

            builder.Services.AddSwaggerGen();
            builder.Services.ApplyApplicationServices();
            builder.Services.AddIdentityService(builder.Configuration);
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Policy", Options =>
                {
                    Options.AllowAnyHeader().AllowAnyMethod().WithOrigins(builder.Configuration["FrontBaseUrl"]);
                    
                });
            });
            
            #endregion

            var app = builder.Build();
  
             # region  Create Scope:
            using var scope = app.Services.CreateScope();
            //Ask For Use DI
            var Services = scope.ServiceProvider;
            //Ask For Object
          
            var loggerfactory = Services.GetRequiredService<ILoggerFactory>();
            try
            {
                //Ask For use DI Explicitly
                var _dbcontext = Services.GetRequiredService<StoreContext>();
                await _dbcontext.Database.MigrateAsync();
                var IdentityContext = Services.GetRequiredService<AppIdentityDbContext>();
                await IdentityContext.Database.MigrateAsync();
                //Ask For use DI Explicitly
                var userManager = Services.GetRequiredService<UserManager<AppUser>>();

                await AppIdentityDbContextSeed.SeedIdentityAsync(userManager);
                await StoreContextSeed.SeedAsync(_dbcontext);
            }
            catch (Exception ex)
            {
                var logger = loggerfactory.CreateLogger<Program>();
                logger.LogError(ex, "An exception has been occured during  apply migration");


            }
            #endregion
            #region  Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMiddleware<ExceptionMiddleWare>();
                app.UseSwagger();
                app.ApplySwaggerMiddleWare();
            }
            app.UseStaticFiles();
            app.UseStatusCodePagesWithReExecute("/error/{0}");
            app.UseHttpsRedirection();
            app.UseCors("Policy");
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
            #endregion
            app.Run();
        }
    }
}
