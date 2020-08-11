using AppDB.Db;
using AppDB.DbOperations;
using AppModel;
using AppModel.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MDL.Controllers
{
    public class AdminController : Controller
    {
        private UserRep ur;
        private ScheduleRepository shr;
        private SubjectRepository sr;
        public AdminController()
        {
            shr = new ScheduleRepository();
            ur = new UserRep();
            sr = new SubjectRepository();

        }
        [Authorize(Roles = "1")]
        public ActionResult Dashboard()
        {
            return View();
        }

        /***************************Users***************************/
        [HttpGet]
        [Authorize(Roles = "1")]
        public ActionResult Register()
        {
            GetAllRoles();
            GetAllRolls();
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "1")]
        public ActionResult Register(UserViewModel uvm)
        {
            GetAllRoles();
            GetAllRolls();
            //var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (!ModelState.IsValid)
            {
                if (ur.IsExist(uvm))
                {
                    TempData["IsExist"] = "Already Added";
                    return RedirectToAction("Register", uvm);
                }

                else
                {
                    SendMail(uvm);
                    ur.Insert(uvm);
                    TempData["IsSuccess"] = "Inserted Successfully";
                    return RedirectToAction("Register", "Admin");
                }
            }
            else
            {
                TempData["IsFail"] = "UnSccessfull";
                return RedirectToAction("Register", "Admin");
            }
        }
        [Authorize(Roles = "1")]
        public ActionResult List()
        {
            var list = ur.GetAll();
            return View(list);
        }
        [Authorize(Roles = "1")]
        public ActionResult GetById(int id)
        {
            GetAllRoles();
            var rcrd = ur.GetById(id);
            return View(rcrd);
        }
        [Authorize(Roles = "1")]
        [HttpPost]
        public ActionResult GetById(int id, UserViewModel uvm)
        {
            GetAllRoles();
            if (ModelState.IsValid)
            {
                ur.Update(id, uvm);
                return RedirectToAction("List", "Admin");
            }
            return View();
        }
        [Authorize(Roles = "1")]
        
        public ActionResult Delete(int Delid)
        {
            ur.Delete(Delid);
            return RedirectToAction("List", "Admin");
        }
        
        private void SendMail(UserViewModel uvm)
        {
            WebMail.Send(uvm.user_email
                        , "**Account Registration**"
                        //mail message body
                        , CreateBody(uvm.user_name,uvm.user_email,uvm.user_pwd)
                        , null
                        , null
                        , null
                        , true
                        , null
                        , null
                        , null
                        , null
                        , null
                        );
        }
        //values binding with html page for mail body
        private string CreateBody(string name,string email,string pwd) {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/Views/UserRegistrationInfo.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{user_name}", name);
            body = body.Replace("{user_email}", email);
            body = body.Replace("{user_pwd}", pwd);
            return body;
        }
        private void GetAllRoles()
        {
            var list = ur.GetRoles();
            SelectList getRole = new SelectList(list, "role_id", "role_name");
            ViewBag.RoleListName = getRole;
        }
        private void GetAllRolls()
        {
            var stdRoll = ur.GetRoll();
            var data = (from obj in stdRoll select new { obj.user_id, obj.tbl_Student.FirstOrDefault().std_rollNo });
            SelectList getAllRoll = new SelectList(data, "user_id", "std_rollNo");
            ViewBag.AllRolls = getAllRoll;
        }

        /***************************Subjects***************************/
        [Authorize(Roles = "1")]
        [HttpGet]
        public ActionResult InsertSubject()
        {
            return View();
        }
        [Authorize(Roles = "1")]
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult InsertSubject(SubjectViewModel svm)
        {
            if (ModelState.IsValid)
            {
                if (sr.IsExist(svm))
                {
                    TempData["IsExist"] = "Already Added";
                    return RedirectToAction("InsertSubject", svm);
                }

                else
                {
                    sr.InsertSubject(svm);
                    TempData["IsSuccess"] = "Inserted Successfully";
                    return RedirectToAction("InsertSubject", "Admin");
                }
            }
            else
            {
                TempData["IsFail"] = "UnSccessfull";
                return RedirectToAction("InsertSubject", "Admin");
            }
        }
        [Authorize(Roles = "1")]
        public ActionResult SubjectList()
        {
            var list = sr.SubjectList();
            return View(list);
        }

        [Authorize(Roles = "1")]
        public ActionResult GetSubById(int id)
        {
            var rcrd = sr.GetById(id);
            return View(rcrd);
        }
        [Authorize(Roles = "1")]
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult GetSubById( SubjectViewModel svm)
        {
            if (ModelState.IsValid)
            {
                sr.UpdateSubject(svm.subj_id, svm);
                return RedirectToAction("SubjectList", "Admin");
            }
            return View();
        }
        [Authorize(Roles = "1")]
        public ActionResult DeleteSub(int id)
        {
            sr.DeleteSubject(id);
            return RedirectToAction("SubjectList", "Admin");
        }

        /***************************Schedule***************************/

        [Authorize(Roles = "1")]
        [HttpGet]
        public ActionResult InsertSchedule()
        {
            return View();
        }
        [Authorize(Roles = "1")]
        [HttpPost]
        public ActionResult InsertSchedule(ScheduleViewModel svm)
        {
            if (ModelState.IsValid)
            {
                if (shr.IsExist(svm))
                {
                    TempData["IsExist"] = "Already Added";
                    return RedirectToAction("InsertSchedule", svm);
                }
                else
                {
                    shr.Insert(svm);
                    TempData["IsSuccess"] = "Inserted Successfully";
                    return RedirectToAction("InsertSchedule", "Admin");
                }
                
                
            }
            else
            {
                TempData["IsFail"] = "UnSccessfull";
                return RedirectToAction("InsertSchedule", "Admin");
            }
        }

        [Authorize(Roles = "1")]
        public ActionResult ScheduleList()
        {
            var list = shr.ScheduleList();
            return View(list);
        }
        [Authorize(Roles = "1")]
        public ActionResult GetScheduleById(int id)
        {
            var rcrd = shr.GetById(id);
            return View(rcrd);
        }

        [Authorize(Roles = "1")]
        [HttpPost]
        public ActionResult GetScheduleById(ScheduleViewModel svm)
        {
            if (ModelState.IsValid)
            {
                shr.UpdateSchedule(svm.sheduleId, svm);
                TempData["IsSuccess"] = "Updated Successfully";
               
            }
            else
            {
                TempData["IsFail"] = "UnSccessfull";
            }

            return RedirectToAction("ScheduleList", "Admin");
        }
        [Authorize(Roles = "1")]
        public ActionResult DeleteSchedule(int id)
        {
            shr.DeleteSchdule(id);
            return RedirectToAction("ScheduleList", "Admin");
        }
    }
}
