namespace Talabt.API.Errors
{
    public class ApiExceptionResponse:ApiResponses
    {
        public string? Details {  get; set; }   
      public ApiExceptionResponse(int Statuscode,string? Message=null,string? details=null) : base(Statuscode, Message)

        {
            Details = details;
        }
       

    }
}
