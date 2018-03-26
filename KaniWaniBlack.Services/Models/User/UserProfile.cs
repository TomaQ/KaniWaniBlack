using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaniWaniBlack.Services.Models.User
{
    public class UserProfile
    {
        public string Username { get; set; }
        public int WaniKaniLevel { get; set; }
        public string Gravatar { get; set; }
        public string WaniKaniApiKey { get; set; }
    }
}