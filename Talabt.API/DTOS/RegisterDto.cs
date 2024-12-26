using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;

namespace Talabt.API.DTOS
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName {  get; set; }
        [Required]
        [EmailAddress]  
        public string Email { get; set; }
        [Required]
        [Phone] 
        public string phoneNumber { get; set; }
        [Required]
        
        public string Password { get; set; }
        
    }
}
