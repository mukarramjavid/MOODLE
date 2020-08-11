using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppModel.ViewModels
{
    public class ParentViewModel
    {
        public int parent_id { get; set; }
        [Display(Name ="Child Roll No")]
        [Required]
        public Nullable<int> UserID { get; set; }
        public Nullable<int> ChildUserID { get; set; }


    }
}
