using Api.Controllers.Base;
using Api.Services.Interfaces;
using Domain.Request;
using Domain.Requests;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Controllers.Users
{
    [Route("user")]
    public class UsersController : BaseController
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("signin")]
        public async Task<IActionResult> Authenticate([FromBody]LoginRequest userRequest)
        {
            var user = await _userService.AuthenticateAsync(userRequest);
            if (user?.Id == Guid.Empty)
                return null;

            // Create claims principal
            var claimsPrincipal = new ClaimsPrincipal(
                new ClaimsIdentity(new[]{
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.NickName),
            }, CookieAuthenticationDefaults.AuthenticationScheme));

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            return Ok(claimsPrincipal.Identity);
        }

        [AllowAnonymous]
        [HttpGet("users")]
        public async Task<IActionResult> GetUser([FromBody]UserRequest userRequest)
        {
            var user = await _userService.GetAllByAsync(userRequest);

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<IActionResult> SingUp([FromBody]UserRequest userRequest)
        {
            var user = await _userService.AddAsync(userRequest);

            return Ok(user);
        }
    }
}