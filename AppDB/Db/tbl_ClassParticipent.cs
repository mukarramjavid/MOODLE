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
    
    public partial class tbl_ClassParticipent
    {
        public int Class_Part_id { get; set; }
        public Nullable<int> StdUserID { get; set; }
        public Nullable<int> ClassID { get; set; }
        public string ClassName { get; set; }
    
        public virtual tbl_Class tbl_Class { get; set; }
        public virtual tbl_User tbl_User { get; set; }
    }
}
