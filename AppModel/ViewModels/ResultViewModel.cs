using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppModel.ViewModels
{
    public class ResultViewModel
    {
        public int result_id { get; set; }
        [Display(Name ="Grade ")]
        [Required(ErrorMessage ="Grade is required.")]
        public string std_grades { get; set; }
        public Nullable<int> subjID { get; set; }
        public Nullable<int> userID { get; set; }

        public virtual SubjectViewModel tbl_Subject { get; set; }
        public virtual UserViewModel tbl_User { get; set; }
    }
}
