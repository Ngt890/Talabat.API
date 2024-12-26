using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.identity
{
    public  class AppUser:IdentityUser

    {
        public string DisplayName {  get; set; }
        //Address
        [JsonIgnore]
        public AddAddress Addresses { get; set; } 
    }
}
