using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaniWaniBlack.Services.Models.WaniKani
{
    public class WaniKaniVocabWords
    {
        public UserInformation user_information { get; set; }
        public List<RequestedInformation> requested_information { get; set; }
        public Error error { get; set; }
    }

    public class UserWaniKaniVocabWords
    {
        public UserInformation user_information { get; set; }
        public UserRequestedInformation requested_information { get; set; }
        public Error error { get; set; }
    }
}