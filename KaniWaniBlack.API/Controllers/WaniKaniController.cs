using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using KaniWaniBlack.Helper.Services;
using KaniWaniBlack.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KaniWaniBlack.API.Controllers
{
    [Produces("application/json")]
    public class WaniKaniController : Controller
    {
        private readonly IWaniKaniService _WKService;

        public WaniKaniController(IWaniKaniService waniServ)
        {
            _WKService = waniServ;
        }

        [Authorize]
        [HttpGet]
        public ActionResult GetUserWaniKaniData()
        {
            string apiKey = HttpHelper.GetClaim(HttpContext.User, Strings.CLAIM_API_KEY);
            int userId = Convert.ToInt32(HttpHelper.GetClaim(HttpContext.User, Strings.CLAIM_USER_ID));
            string application = HttpHelper.GetClaim(HttpContext.User, Strings.CLAIM_APPLICATION);
            Logger.LogInfo("Starting GetUserWaniKaniData action for id: " + userId + " from " + application);

            bool didUpdate = _WKService.GetUserWaniKaniData(userId, apiKey);
            return Json(didUpdate);
        }

        [Authorize]
        [HttpPost]
        public ActionResult UpdateWaniKaniVocabList()
        {
            string userName = HttpHelper.GetClaim(HttpContext.User, JwtRegisteredClaimNames.UniqueName);
            Logger.LogInfo("Starting UpdateWaniKaniVocabList action for " + userName); //TODO: figure out admin rights
            //TODO: check if admin user first or username = something here probably
            string apiKey = HttpHelper.GetClaim(HttpContext.User, Strings.CLAIM_API_KEY);

            bool didUpdate = _WKService.UpdateWaniKaniVocabList(apiKey);
            Logger.LogInfo("Finished UpdateWaniKaniVocabList action for " + userName);
            return Json(didUpdate);
        }
    }
}