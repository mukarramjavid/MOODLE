using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppModel.ViewModels
{
    public class TeacherViewModel
    {
        public int teacher_id { get; set; }
        [Display(Name ="Designation")]
        [Required]
        public string teacher_designation { get; set; }

        public int user_id { get; set; }
    }
}
