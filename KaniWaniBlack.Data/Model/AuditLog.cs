//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KaniWaniBlack.Data.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class AuditLog
    {
        public int ID { get; set; }
        public string Module { get; set; }
        public string Action { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
    }
}
