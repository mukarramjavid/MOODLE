using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppModel.ViewModels
{
    public class PostViewModel
    {
        public int postId { get; set; }
        [Display(Name ="Post Description")]
        [Required]
        public string postDesc { get; set; }
        public Nullable<int> Tchr_User_Id { get; set; }
        public int? classId { get; set; }
        public string teacher_name { get; set; }
        public string subject_name { get; set; }
        
        public string text { set; get; }


    }
}
