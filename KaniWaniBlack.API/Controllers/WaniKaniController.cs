using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KaniWaniBlack.API.Controllers
{
    [Produces("application/json")]
    [Route("api/wanikani")]
    public class WaniKaniController : Controller
    {
        [Authorize]
        public ActionResult GetWaniKaniData()
        {
            return Json("");
        }

        [Authorize]
        public ActionResult GetWaniKaniVocabList()
        {
            return Json("");
        }
    }
}