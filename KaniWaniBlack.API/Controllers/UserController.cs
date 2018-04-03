using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KaniWaniBlack.API.Models;
using KaniWaniBlack.Helper.Services;
using KaniWaniBlack.Services.Models.Authentication;
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
            Logger.LogInfo("Starting create user action for: " + request.Username + " from " + request.Application);
            return Json(_userService.CreateUser(request.Username, request.Password, request.ConfirmPassword, request.Application));
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult LoginUser([FromBody]LoginUserRequest request)
        {
            Logger.LogInfo("Starting login user action for: " + request.Username + " from " + request.Application);
            IActionResult response = Unauthorized();
            AuthenticationResponse isAuthenticated = Authenticate(request.Username, request.Password, request.Application);

            if (isAuthenticated.Code == Services.Models.CodeType.Ok)
            {
                string tokenString = BuildToken(isAuthenticated, request.Application);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        [Authorize]
        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordRequest request)
        {
            Logger.LogInfo("Starting reset password action for: " + request.Username + " from " + request.Application);
            if (request.NewPassword == request.ConfirmNewPassword)
            {
                return Json(_userService.ResetPassword(request.Username, request.Password, request.NewPassword, request.Application));
            }

            return Json(Strings.PASSWORDS_DONT_MATCH);
        }

        [Authorize]
        [HttpGet]
        public ActionResult GetUserProfile()
        {
            //should not throw exception since all tokens should have a user id
            int userId = Convert.ToInt32(HttpHelper.GetClaim(HttpContext.User, Strings.CLAIM_USER_ID));
            return Json(_userService.GetUserProfile(userId));
        }

        [Authorize]
        [HttpPost]
        public ActionResult UpdateUserProfile([FromBody]UpdateUserRequest request)
        {
            int userId = Convert.ToInt32(HttpHelper.GetClaim(HttpContext.User, Strings.CLAIM_USER_ID));
            Logger.LogInfo(string.Format("Starting UpdateUserProfile action for userId: {0}", userId));
            return Json(_userService.UpdateUserProfile(userId, request.Username, request.WaniKaniApiKey));
        }

        private string BuildToken(AuthenticationResponse response, string applicationUsed)
        {
            var claims = new[] {
                new Claim(Strings.CLAIM_USERNAME, response.UserName),
                new Claim(Strings.CLAIM_API_KEY, response.ApiKey ?? ""),
                new Claim(Strings.CLAIM_APPLICATION, applicationUsed),
                new Claim(Strings.CLAIM_USER_ID, response.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Issuer"], claims,
                notBefore: DateTime.Now, expires: DateTime.Now.AddMinutes(30), signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private AuthenticationResponse Authenticate(string username, string password, string applicationUsed)
        {
            return _userService.ValidateUser(username, password, applicationUsed);
        }
    }
}