using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaniWaniBlack.Services.Models.WaniKani
{
    public class VocabWord
    {
        public UserInformation user_information { get; set; }
        public List<RequestedInformation> requested_information { get; set; }
        public Error error { get; set; }
    }
}