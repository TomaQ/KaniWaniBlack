using System;
using System.Collections.Generic;

namespace KaniWaniBlack.Data.Models
{
    public partial class WaniKaniUser
    {
        public int Id { get; set; }
        public string WkApiKey { get; set; }
        public string Gravatar { get; set; }
        public int? Wklevel { get; set; }
        public string Wkusername { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
    }
}