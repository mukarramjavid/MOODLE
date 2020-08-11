using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppModel.ViewModels
{
    public class RoleViewModel
    {

        [Display(Name = "Role Id ")]
        public int role_id { get; set; }
        [Display(Name = "Role ")]
        public string role_name { get; set; }
    }
}
