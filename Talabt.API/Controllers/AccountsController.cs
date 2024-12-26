using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.Core.Entities.identity;
using Talabat.Core.Services;
using Talabt.API.DTOS;
using Talabt.API.Errors;
using System.Security.Principal;
using Talabt.API.Extentions;
using AutoMapper;
using Microsoft.Owin.BuilderProperties;

namespace Talabt.API.Controllers
{

    public class AccountsController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenProvider _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;

        public AccountsController(UserManager<AppUser> userManager,
            ITokenProvider tokenService,
            SignInManager<AppUser> signInManager,
            IMapper mapper)
            
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _mapper = mapper;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> RegisterAsync(RegisterDto model)
        {
            if(CheckEmailExist(model.Email).Result.Value)
            {
                return BadRequest(new ApiResponses(400, "Email Is Already In Use"));
            }
            var user = new AppUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
                PhoneNumber = model.phoneNumber,


            };
            var Result = await _userManager.CreateAsync(user, model.Password);
            if (!Result.Succeeded)



            {
                foreach (var error in Result.Errors)
                {
                    Console.WriteLine($"Error: {error.Description}");
                }




                BadRequest(new ApiResponses(400)); }
            var ReturnedUser = new UserDto()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)


            };
            return Ok(ReturnedUser);


        }


        //login
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> LoginAsync(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null) { return Unauthorized(new ApiResponses(401)); }
            var Result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!Result.Succeeded) { return Unauthorized(new ApiResponses(401)); }
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            });


        }


        //
        //api/Accounts/GetCurrentUser
        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {


            //var Email = User.FindFirstValue(ClaimTypes.Email);
            var email = User.FindFirstValue(ClaimTypes.Email); 
            // يجب التأكد من أن ClaimTypes.Email هو الذي تم إضافته أثناء إنشاء التوكن
            if (email == null)
            {
                return Unauthorized("Email claim not found in the token");
            }
            //User.FindFirstValue(ClaimTypes.Email);           
            var user = await _userManager.FindByEmailAsync(email);
            var Returneduser = new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            };
            return Ok(Returneduser);


        }



        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress()
        {
            
            var user = await _userManager.FindUserWithAddressAsync(User);
            var MappedAddress = _mapper.Map<AddAddress, AddressDto>(user.Addresses);
            return Ok(MappedAddress);


        }

        [Authorize]
        [HttpPut("Address")]
        //this is for update
        public async Task<ActionResult<AddressDto>> UpdateAddres(AddressDto UpdatedAddress)
        {
          
            var user = await _userManager.FindUserWithAddressAsync(User);
            var MappedAddress=_mapper.Map<AddressDto,AddAddress>(UpdatedAddress);
            MappedAddress.Id= user.Addresses.Id;
            user.Addresses = MappedAddress;  //update
            var Result = await _userManager.UpdateAsync(user);
            if (!Result.Succeeded) { return BadRequest(new ApiResponses(400)); }
            return Ok(UpdatedAddress);
            
        }

        [HttpGet("EmailExist")]
        public async Task<ActionResult<bool>> CheckEmailExist(string Email)
        {
            return await _userManager.FindByEmailAsync(Email) is not null;
        }



    }

}



    
