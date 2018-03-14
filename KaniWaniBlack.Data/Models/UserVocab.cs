using System;
using System.Collections.Generic;

namespace KaniWaniBlack.Data.Models
{
    public partial class UserVocab
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int WkvocabId { get; set; }
        public bool IsBurned { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string UserSynonyms { get; set; }

        public User User { get; set; }
        public WaniKaniVocab Wkvocab { get; set; }
    }
}
