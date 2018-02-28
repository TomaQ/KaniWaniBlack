using System;
using System.Collections.Generic;

namespace KaniWaniBlack.Data.Models
{
    public partial class User
    {
        public User()
        {
            UserVocab = new HashSet<UserVocab>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public string ApiKey { get; set; }
        public string LastApplicationUsed { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public int? LoginAttempts { get; set; }
        public DateTime? LastLoginAttempt { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public ICollection<UserVocab> UserVocab { get; set; }
    }
}