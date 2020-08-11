using AppDB.DbOperations;
using AppModel;
using AppModel.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;



namespace MDL.Controllers
{
    public class StudentController : Controller
    {
        private int id;
        private ResultRepository rr;
        private ClassRepository cr;
        private ClassParticipentRepository cpr;
        public StudentController()
        {
            cr = new ClassRepository();
            rr = new ResultRepository();
            cpr = new ClassParticipentRepository();
        }
        [Authorize(Roles = "2")]
        public ActionResult StudentDashboard()
        {
            return View();
        }
        [Authorize(Roles = "2")]
        public ActionResult View_Result()
        {
            id = GetId();
            var list = rr.GetStdList().Where(x => x.userID.ToString() == id.ToString()).ToList();
            return View(list);
        }

        /*************************************Join Classes*****************************************************/
        [Authorize(Roles = "2")]
        [HttpGet]
        public ActionResult JoinClass()
        {
            id = GetId();
            var list = cpr.GetAllPartiClass().Where(x => x.StdUserID == id).ToList();
            return View(list);
        }
        [Authorize(Roles = "2")]
        [HttpPost]
        public ActionResult JoinClass(string code)
        {
            var classCode = cr.ClassList();
            var data = classCode.Where(x => x.class_code == code).ToList();
            //data bindig with ClassViewModel

            ClassViewModel cvm = new ClassViewModel();
            id = GetId();
            if (data == null)
            {
                TempData["Error"] = "Code is Invalid";
                return View();
            }
            else
            {
                cvm.class_id = data.FirstOrDefault().class_id;
                cvm.class_name = data.FirstOrDefault().class_name;
                
                var IsTrue = data.FirstOrDefault().class_code == code;
                //Redirect to Class 

                if (IsTrue)
                {
                    cpr.InsertParticipent(cvm, id);
                    var list = cpr.GetAllPartiClass().Where(x => x.StdUserID == id).ToList();
                    return View(list);
                }
                else
                {
                    return View("false");
                }
            }

            

        }
        private int GetId()
        {
            return Convert.ToInt32(Session["id"]);
        }



    }
}