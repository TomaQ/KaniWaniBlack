using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaniWaniBlack.Services.Models
{
    public class BaseResponse
    {
        public string Message { get; set; }
        public string Status { get; set; }
        public CodeType Code { get; set; }

        public BaseResponse()
        {
            this.Message = "An error occured";
            this.Status = "Initialized";
            this.Code = CodeType.Error;
        }
    }
    
    public enum CodeType
    {
        Ok = 1,
        Error = 2,
        BadRequest = 3,
        SqlError = 4
    }
}
