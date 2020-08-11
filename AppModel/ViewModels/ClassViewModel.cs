using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppModel.ViewModels
{
    public class ClassViewModel
    {
        public int class_id { get; set; }
        [Display(Name ="Class Name")]
        [Required(ErrorMessage ="This field is required.")]
        public string class_name { get; set; }

        [Display(Name = "Class Code")]
        [Required(ErrorMessage = "This field is required.")]
        public string class_code { get; set; }

        public int? TeachUserId { get; set; }

    }
}
