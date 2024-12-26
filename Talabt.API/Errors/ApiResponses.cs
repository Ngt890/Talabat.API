
namespace Talabt.API.Errors
{
    public class ApiResponses
    {

        public int Statuscode { get; set; }

        public string? Message { get; set; }
        public ApiResponses(int statuscode, string? message = null)
        {

            Statuscode = statuscode;
            Message = message ?? GetDefaultMessage(Statuscode);
        }

        private string? GetDefaultMessage(int statuscode)
        {
            //BadRequest  ----400
            //NotFound  ---404
            //interenalserverError--500
            //unautherized--401
            return statuscode switch
            {
                400 => "BadRequest",
                404 => "NotFound",
                500 => "InternalServerError",
                401 => "UnAutherized",
                _ => null


            };
        }
    }
}
