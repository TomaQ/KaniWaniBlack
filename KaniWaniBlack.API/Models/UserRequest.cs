using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KaniWaniBlack.API.Models
{
    public class UserRequest : BaseRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}