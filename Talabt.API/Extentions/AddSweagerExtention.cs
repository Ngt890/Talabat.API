namespace Talabt.API.Extentions
{
    public  static class AddSweagerExtention
    {
        public static WebApplication ApplySwaggerMiddleWare(this WebApplication app)
        {

            
            app.UseSwagger();
            app.UseSwaggerUI();

            return app;
        }
    }
}
