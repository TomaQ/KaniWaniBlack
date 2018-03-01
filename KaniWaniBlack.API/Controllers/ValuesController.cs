using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KaniWaniBlack.Helper.Services;

namespace KaniWaniBlack.API.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet, Authorize]
        public IEnumerable<string> Get()
        {
            int? userAge = 0;
            try
            {
                var currentUser = HttpContext.User;

                DateTime? birthDate = DateTime.Parse(HttpHelper.GetClaim(currentUser, ClaimTypes.DateOfBirth));
                userAge = DateTime.Today.Year - birthDate?.Year; //not real/accurate calculation
            }
            catch (Exception ex)
            {
                Logger.HandleException(ex);
            }

            if (userAge < 18)
            {
                return new string[] { "not old enough", "try later" };
            }

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}