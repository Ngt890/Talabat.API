using Microsoft.Extensions.Logging;
using System.Text.Json;
using Talabt.API.Errors;



namespace Talabt.API.MiddleWares
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleWare> _logger;
        private readonly IHostEnvironment  _env;

        public ExceptionMiddleWare(RequestDelegate Next ,ILogger<ExceptionMiddleWare> logger,IHostEnvironment env)
        {
            _next = Next;
            _logger = logger;
            _env = env;
        }

        //AnyMiddleware --------> InvokeAsync();
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex) 
            {//logerror in DB  -- >Production 
                _logger.LogError(ex,ex.Message);
                //identify 
                context.Response.ContentType = "text/json";
                context.Response.StatusCode = 500;
                
                
                 var Response= _env.IsDevelopment()?new ApiExceptionResponse(500,ex.Message,ex.StackTrace.ToString()): new ApiExceptionResponse(500);
                var jsonresponse = JsonSerializer.Serialize(Response);
              await  context.Response.WriteAsync(jsonresponse);


            }
 
        }


    }
}
