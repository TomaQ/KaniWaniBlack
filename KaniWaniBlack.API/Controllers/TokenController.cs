using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KaniWaniBlack.API.Models;
using KaniWaniBlack.Services.Services.Interfaces;
using KaniWaniBlack.Data.Models;

namespace KaniWaniBlack.API.Controllers
{
    [Route("api/Token")]
    public class TokenController : Controller
    {
        private IConfiguration _config;
        private readonly IUserService _userService;

        public TokenController(IConfiguration config, IUserService userService)
        {
            _config = config;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateToken([FromBody]LoginRequest login)
        {
            IActionResult response = Unauthorized();
            var user = Authenticate(login);

            if (user != null)
            {
                string tokenString = BuildToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        private string BuildToken(User user) //TODO: clean this up
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim("api_key", user.ApiKey),
                new Claim(JwtRegisteredClaimNames.Birthdate, user.CreatedOn.ToString("yyyy-MM-dd")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Issuer"], claims,
                notBefore: DateTime.Now, expires: DateTime.Now.AddMinutes(30), signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private User Authenticate(LoginRequest login)
        {
            User user = null;
            User testUser = _userService.GetUserById(1); //TODO: not this

            if (login.Username == "TestUser" && login.Password == "testPassword")
            {
                user = new User { Username = "TestUser", ApiKey = "testAkiKey" };
            }
            return user;
        }
    }
}