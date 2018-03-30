using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KaniWaniBlack.API.Models
{
    public class UpdateUserRequest
    {
        public string Username { get; set; }
        public string WaniKaniApiKey { get; set; }
    }
}