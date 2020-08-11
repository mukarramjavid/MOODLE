using AppModel.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AppModel
{
    public class UserViewModel
    {
        public int user_id { get; set; }
        [Display(Name = "UserName")]
        public string user_name { get; set; }

        //[Required(ErrorMessage = "Email Field is required")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string user_email { get; set; }

        //[Required(ErrorMessage = "Password Field is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string user_pwd { get; set; }

        [Display(Name = "Role")]
        public int role_id { get; set; }

        public string ImagePath { get; set; }
        //To accept Image File from View
        public HttpPostedFileBase UserImageFile { get; set; }

        public int? parent_id { get; set; }

        public RoleViewModel tbl_Role { get; set; }
        public StudentViewModel student { get; set; }
        public TeacherViewModel teacher { get; set; }
        public ParentViewModel parent { get; set; }
       


    }
}
