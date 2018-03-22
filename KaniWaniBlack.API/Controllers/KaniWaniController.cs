using System;
using System.Collections.Generic;
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
    public class KaniWaniController : Controller
    {
        private readonly IKaniWaniService _kaniWaniService;

        public KaniWaniController(IKaniWaniService kaniwaniServ)
        {
            _kaniWaniService = kaniwaniServ;
        }

        [HttpGet]
        [Authorize]
        public ActionResult TestGetKaniWaniQueue()
        {
            int userId = Convert.ToInt32(HttpHelper.GetClaim(HttpContext.User, Strings.CLAIM_USER_ID));
            var vocabList = _kaniWaniService.TestGetKaniWaniVocabList(userId);

            return Json(vocabList);
        }
    }
}