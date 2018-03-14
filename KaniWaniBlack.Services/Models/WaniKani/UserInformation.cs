using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaniWaniBlack.Services.Models.WaniKani
{
    public class UserInformation
    {
        public string username { get; set; }
        public string gravatar { get; set; }
        public int level { get; set; }
        public string title { get; set; }
        public string about { get; set; }
        public object website { get; set; }
        public object twitter { get; set; }
        public int topics_count { get; set; }
        public int posts_count { get; set; }
        public int creation_date { get; set; }
        public object vacation_date { get; set; }
    }
}