using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public ActionResult GetUserWaniKaniData(string apiKey)
        {
            _WKService.Test(apiKey);
            return Json("");
        }

        [Authorize]
        public ActionResult GetWaniKaniVocabList()
        {
            return Json("");
        }
    }
}