using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppModel.ViewModels
{
    public class StudentViewModel
    {
        [Display(Name = "Roll No")]
        [Required(ErrorMessage ="Std Roll field is required")]
        public string std_rollNo { get; set; }

        [Display(Name = "Deppt")]
        [Required(ErrorMessage = "Depp field is required")]
        public string std_deppt { get; set; }

        [Display(Name = "Degree")]
        [Required(ErrorMessage = "Degree field is required")]
        public string std_degree { get; set; }

        public int user_id { get; set; }
    }
}
