using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaniWaniBlack.Services.Models.WaniKani
{
    public class UserSpecific
    {
        public string srs { get; set; }
        public int srs_numeric { get; set; }
        public int unlocked_date { get; set; }
        public int available_date { get; set; }
        public bool burned { get; set; }
        public int burned_date { get; set; }
        public int meaning_correct { get; set; }
        public int meaning_incorrect { get; set; }
        public int meaning_max_streak { get; set; }
        public int meaning_current_streak { get; set; }
        public int reading_correct { get; set; }
        public int reading_incorrect { get; set; }
        public int reading_max_streak { get; set; }
        public int reading_current_streak { get; set; }
        public object meaning_note { get; set; }
        public object reading_note { get; set; }
        public object user_synonyms { get; set; }
    }
}