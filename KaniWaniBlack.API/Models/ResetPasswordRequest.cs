using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KaniWaniBlack.API.Models
{
    public class ResetPasswordRequest : UserRequest
    {
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}