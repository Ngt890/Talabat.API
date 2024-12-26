﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.identity;

namespace Talabat.Core.Services
{
    public  interface ITokenProvider
    {
        Task<String> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager);
    }
}
