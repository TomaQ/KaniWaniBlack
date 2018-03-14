using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaniWaniBlack.Services.Models.WaniKani
{
    public class RequestedInformation
    {
        public int level { get; set; }
        public string character { get; set; }
        public string kana { get; set; }
        public string meaning { get; set; }
        public UserSpecific user_specific { get; set; }
    }
}