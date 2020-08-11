using AppDB.DbOperations;
using AppModel;
using AppModel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MDL.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private UserRep ur;
        private ResultRepository rr;
        public AccountController()
        { 
            rr = new ResultRepository();
            ur = new UserRep();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserViewModel uvm)
        {
            var count = ur.Login(uvm);
            if (ModelState.IsValid)
            {
                if (count != null)
                {
                    Session["id"] = count.user_id;
                    Session["N"] = count.user_name.ToUpper();
                    if (count.ImagePath == null)
                    {
                        Session["img"] = "~/images/no_image_available_male.png";
                    }
                    else
                    {
                        Session["img"] = count.ImagePath;
                    }
                   
                    var c = count.role_id;
                    
                    FormsAuthentication.SetAuthCookie(uvm.user_email, false);
                    if (c == 1)
                    {
                        return RedirectToAction("Dashboard", "Admin", Session["N"]);
                    }
                    else if (c == 2)
                    {
                        return RedirectToAction("StudentDashboard", "Student", Session["N"]);
                    }
                    else if (c == 3)
                    {
                        return RedirectToAction("TeacherDashboard", "Teacher", Session["N"]);
                    }
                    else if (c == 4)
                    {
                        return RedirectToAction("ParentDashboard", "Parent", Session["N"]);
                    }

                    else
                    {
                        return View();
                    }

                }
                else
                {
                    TempData["Error"] = "Invalid Email or Password";
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}