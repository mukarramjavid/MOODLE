//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppDB.Db
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Student
    {
        public string std_rollNo { get; set; }
        public string std_deppt { get; set; }
        public string std_degree { get; set; }
        public int std_id { get; set; }
        public Nullable<int> userID { get; set; }
    
        public virtual tbl_User tbl_User { get; set; }
    }
}
