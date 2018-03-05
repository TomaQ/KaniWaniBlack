using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KaniWaniBlack.API.Models
{
    public class CreateUserRequest : UserRequest
    {
        public string ConfirmPassword { get; set; }
    }
}