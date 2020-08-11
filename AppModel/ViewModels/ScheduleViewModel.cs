using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppModel.ViewModels
{
    public class ScheduleViewModel
    {
        public int sheduleId { get; set; }
        [Display(Name = "Subject Name")]
        [Required(ErrorMessage = "This field is required.")]
        public string subjectName { get; set; }

        [Display(Name = "Date")]
        [Required(ErrorMessage = "This field is required.")]
        [DataType(DataType.Date)]
        public string date { get; set; }

        [Display(Name = "Room No.")]
        [Required(ErrorMessage = "This field is required.")]
        public string room { get; set; }

        [Display(Name = "Teacher Name")]
        [Required(ErrorMessage = "This field is required.")]
        public string teacher { get; set; }

        [Display(Name = "Department")]
        [Required(ErrorMessage = "This field is required.")]
        public string deppt { get; set; }

        [Display(Name = "Session")]
        [Required(ErrorMessage = "This field is required.")]
        public string session { get; set; }

        [Display(Name = "Section")]
        [Required(ErrorMessage = "This field is required.")]
        public string section { get; set; }

        [Display(Name = "Time")]
        [Required(ErrorMessage = "This field is required.")]
        public string time { get; set; }
    }
}
