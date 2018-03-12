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
        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("gravatar")]
        public string Gravatar { get; set; }

        [JsonProperty("level")]
        public short Level { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("about")]
        public string About { get; set; }

        [JsonProperty("website")]
        public string Website { get; set; }

        [JsonProperty("twitter")]
        public string Twitter { get; set; }

        [JsonProperty("topics_count")]
        public int TopicsCount { get; set; }

        [JsonProperty("posts_count")]
        public int PostsCount { get; set; }

        [JsonProperty("creation_date")]
        public DateTime? CreatedDate { get; set; }

        [JsonProperty("vacation_date")]
        public DateTime? VacationDate { get; set; }
    }
}