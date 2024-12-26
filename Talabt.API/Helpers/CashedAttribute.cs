using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using Talabat.Core.Services;

namespace Talabt.API.Helpers
{
    public class CashedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timetolive;

        public CashedAttribute(int  Timetolive)
        {
            _timetolive = Timetolive;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //Ask CLR for injecting object EXPLICITLY
          var CasheService= context.HttpContext.RequestServices.GetRequiredService<IResponseCasheService>();
            var CashKey=GenerateKashKey(context.HttpContext.Request);
           var response= await CasheService.GetCashedData(CashKey);
            if (!string.IsNullOrEmpty(response)) 
            {
                //ReturnDataFrom Redis
                var result = new ContentResult()
                {
                    Content = response,
                    ContentType="application/json",
                   StatusCode=200
                };
                context.Result = result;
                return;
            }
            var ExecutedAction =  await next.Invoke();
            if ( ExecutedAction.Result is OkObjectResult  okObjectResult && ExecutedAction is not  null ) 
            {
            await  CasheService.CasheResponseAsync(CashKey, okObjectResult.Value ,TimeSpan.FromSeconds(_timetolive));
            }


        }

        private  string GenerateKashKey(HttpRequest request)
        {
            var keybuilder = new StringBuilder();
            keybuilder.Append(request.Path);               //api/product
            // sort,pageindex,pagesize
            foreach(var (key,value) in request.Query.OrderBy(x=>x.Key))
            {
                keybuilder.Append($"|{key}-{value}");

            }
            return keybuilder.ToString();   
        }
    }
}
 