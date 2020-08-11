using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppModel.ViewModels
{
    public class SubjectViewModel
    {
        [Display(Name = "Subject ID")]
        public int subj_id { get; set; }

        [Display(Name ="Subject")]
        [Required(ErrorMessage = "This field is required.")]
        public string subj_name { get; set; }

        [Display(Name = "Credits Hour")]
        [Required(ErrorMessage = "This field is required.")]
        public string subj_hrs { get; set; }
    }
}
