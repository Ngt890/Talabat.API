using System.ComponentModel.DataAnnotations;

namespace Talabt.API.DTOS
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email {  get; set; }  
        [Required]  
        public string Password { get; set; }
    }
}
