using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaniWaniBlack.Services.Models.KaniWani
{
    public class UserKaniWaniList
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Level { get; set; }

        public string Character { get; set; }
        public string Kana { get; set; }
        public string Meaning { get; set; }
    }
}