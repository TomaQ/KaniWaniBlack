using System;
using System.Collections.Generic;

namespace KaniWaniBlack.Data.Models
{
    public partial class AuditLogs
    {
        public int Id { get; set; }
        public string Module { get; set; }
        public string Action { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}