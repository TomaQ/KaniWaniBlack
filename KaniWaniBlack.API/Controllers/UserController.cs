using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KaniWaniBlack.API.Models;
using KaniWaniBlack.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace KaniWaniBlack.API.Controllers
{
    [Produces("application/json")]
    //[Route("api/[controller]")]
    public class UserController : Controller
    {
        private IConfiguration _config;
        private readonly IUserService _userService;

        public UserController(IConfiguration config, IUserService userService)
        {
            _config = config;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult CreateUser([FromBody]CreateUserRequest request)
        {
            //TOOD: logging
            return Json(_userService.CreateUser(request.Username, request.Password, request.ConfirmPassword, request.Application));
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult LoginUser([FromBody]LoginUserRequest request)
        {
            IActionResult response = Unauthorized();
            bool isAuthenticated = Authenticate(request);

            if (isAuthenticated)
            {
                string tokenString = BuildToken(request.Username);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        [Authorize]
        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordRequest request)
        {
            return Json("");
        }

        private string BuildToken(string username)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.UniqueName, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Issuer"], claims,
                notBefore: DateTime.Now, expires: DateTime.Now.AddMinutes(30), signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool Authenticate(LoginUserRequest request)
        {
            if (_userService.ValidateUser(request.Username, request.Password, request.Application).Code == Services.Models.CodeType.Ok)
            {
                return true;
            }

            return false;
        }
    }
}