using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Repositories.Contract;
using Talabt.API.Helpers;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;
using Talabat.Repository;

using Talabt.API.Errors;
using Talabat.Core;
using Talabat.Core.Services;
using Talabat.Service;

namespace Talabt.API.Extentions
{
    public static class ApplicationServices

    {

        public static IServiceCollection ApplyApplicationServices(this IServiceCollection Services)
        {

            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            Services.AddScoped(typeof(IUnitOfWork),typeof(UnitOfWork));
            Services.AddScoped(typeof(IBasketRepository),typeof(BasketRepository));
            Services.AddScoped(typeof(IOrderServices), typeof(OrderService));
            Services.AddScoped(typeof(IPaymentServices),typeof(PaymentServices)); 
            Services.AddSingleton<IResponseCasheService, ResponseCasheService>();
            Services.AddSingleton<System.Collections.Hashtable>();
            Services.AddAutoMapper(typeof(MappingProfile));
           

            
            Services.Configure<ApiBehaviorOptions>(options =>

            {
                options.InvalidModelStateResponseFactory = (actioncontext) =>
                {


                    var errors = actioncontext.ModelState.Where(E => E.Value.Errors.Count() > 0)
                                                       .SelectMany(E => E.Value.Errors)
                                                       .Select(E => E.ErrorMessage)
                                                       .ToList();
                    var validationresponse = new ApiValidationErrors()
                    {
                        Errors = errors


                    };
                    return new BadRequestObjectResult(validationresponse);





                };
            });

            return Services;
        }
    }

}










