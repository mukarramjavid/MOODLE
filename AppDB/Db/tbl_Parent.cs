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
    
    public partial class tbl_Parent
    {
        public int parent_id { get; set; }
        public Nullable<int> ChildUserID { get; set; }
        public Nullable<int> UserId { get; set; }
    
        public virtual tbl_User tbl_User { get; set; }
        public virtual tbl_User tbl_User1 { get; set; }
    }
}