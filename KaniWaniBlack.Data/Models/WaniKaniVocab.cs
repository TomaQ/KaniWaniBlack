using System;
using System.Collections.Generic;

namespace KaniWaniBlack.Data.Models
{
    public partial class WaniKaniVocab
    {
        public WaniKaniVocab()
        {
            UserVocab = new HashSet<UserVocab>();
        }

        public int Id { get; set; }
        public int Level { get; set; }
        public string Character { get; set; }
        public string Kana { get; set; }
        public string Meaning { get; set; }

        public ICollection<UserVocab> UserVocab { get; set; }
    }
}
