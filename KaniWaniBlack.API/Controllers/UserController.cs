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
            bool isAuthenticated = Authenticate(request.Username, request.Password, request.Application);

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
            if (request.NewPassword == request.ConfirmNewPassword)
            {
                return Json(_userService.ResetPassword(request.Username, request.Password, request.NewPassword, request.Application));
            }

            return Json("Passwords do not match."); //TODO: static string
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

        private bool Authenticate(string username, string password, string applicationUsed)
        {
            if (_userService.ValidateUser(username, password, applicationUsed).Code == Services.Models.CodeType.Ok)
            {
                return true;
            }

            return false;
        }
    }
}