using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KaniWaniBlack.API.Models
{
    public class UserRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Application { get; set; }
    }
}