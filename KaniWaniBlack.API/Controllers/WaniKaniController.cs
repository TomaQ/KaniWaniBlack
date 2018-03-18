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
    public class WaniKaniController : Controller
    {
        private readonly IWaniKaniService _WKService;

        public WaniKaniController(IWaniKaniService waniServ)
        {
            _WKService = waniServ;
        }

        //[Authorize] //TODO: uncomment
        [HttpGet]
        public ActionResult GetUserWaniKaniData()
        {
            string apiKey = "";
            int userId = 0;
            bool didUpdate = _WKService.GetUserWaniKaniData(userId, apiKey);
            return Json(didUpdate);
        }

        //[Authorize] //TODO: uncomment also
        [HttpPost]
        public ActionResult UpdateWaniKaniVocabList()
        {
            //check if admin user first or username = something here probably
            string apiKey = "";
            bool didUpdate = _WKService.UpdateWaniKaniVocabList(apiKey);
            return Json(didUpdate);
        }
    }
}