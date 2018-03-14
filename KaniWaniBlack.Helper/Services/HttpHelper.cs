using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KaniWaniBlack.Helper.Services
{
    public static class HttpHelper
    {
        public static string GetClaim(ClaimsPrincipal user, string claimType)
        {
            return user.HasClaim(x => x.Type == claimType) ? user.Claims.FirstOrDefault(x => x.Type == claimType).Value : "";
        }
    }
}