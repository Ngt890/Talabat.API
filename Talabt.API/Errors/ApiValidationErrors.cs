namespace Talabt.API.Errors
{
    public class ApiValidationErrors:ApiResponses
    {
        public IEnumerable<string> Errors { get; set; }
        public ApiValidationErrors():base(400)
        {

            Errors = new List<string>();
            
        }
    }
}
