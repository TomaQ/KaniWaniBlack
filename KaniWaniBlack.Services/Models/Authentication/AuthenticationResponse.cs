using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaniWaniBlack.Services.Models.Authentication
{
    public class AuthenticationResponse : BaseResponse
    {
        public string UserName { get; set; }
        public string ApiKey { get; set; }

        public AuthenticationResponse(string u)
        {
            UserName = u;
            Message = "An error occurred";
            Status = "Application Error";
            Code = CodeType.Error;
        }
    }
}
