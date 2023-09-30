using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using NZWalks.API.Repositories.Interfaces;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _token;

        public AuthController(UserManager<IdentityUser> userManager,ITokenRepository token)
        {
            _userManager = userManager;
            _token = token;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.UserName,
            };

            var identityResult=await _userManager.CreateAsync(identityUser, registerDto.Password);
            
            if(identityResult.Succeeded)
            {
                if(registerDto.Roles!=null && registerDto.Roles.Any())
                {
                    identityResult = await _userManager.AddToRolesAsync(identityUser, registerDto.Roles);
                
                    if (identityResult.Succeeded)
                    {
                        return Ok("User registered Successfully!");
                    }
                }
               
            }

            return BadRequest("There is something wrong");


        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromForm] LoginDto login)
        {
            var user = await _userManager.FindByEmailAsync(login.UserName);
            if (user != null)
            {
                var checkPassword = await _userManager.CheckPasswordAsync(user, login.Password);
                if (checkPassword)
                {
                    //Get Roles of the user
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        var jwtToken = _token.CreateJWTToken(user, roles.ToList());
                        return Ok(jwtToken);
                    }
                }

            }
            return BadRequest("Something is wrong");
        }
        
    }
}
