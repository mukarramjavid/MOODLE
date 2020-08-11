using AppDB.DbOperations;
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
    public class TeacherController : Controller
    {
        private ResultRepository rr;
        private UserRep ur;
        private SubjectRepository sr;
        private ClassRepository cr;
        private PostRepository pr;
        /*get user id from account controller*/
        int c;
        public TeacherController()
        {
            rr = new ResultRepository();
            ur = new UserRep();
            cr = new ClassRepository();
            sr = new SubjectRepository();
            pr = new PostRepository();
        }
        [Authorize(Roles = "3")]
        public ActionResult TeacherDashboard()
        {
            return View();
        }
        /**********************************Classes*************************************************/
        [Authorize(Roles = "3")]
        [HttpGet]
        public ActionResult CreateClass()
        {
            return View();
        }
        [Authorize(Roles = "3")]
        [HttpPost]
        public ActionResult CreateClass(ClassViewModel cvm)
        {
            if (ModelState.IsValid)
            {
                c = GetId();
                cr.Create(cvm, c);
                //TempData["IsSuccess"] = "Inserted Successfully";
                return RedirectToAction("ClassList", "Teacher");


            }
            else
            {
                TempData["IsFail"] = "UnSccessfull";
                return RedirectToAction("CreateClass", "Teacher");

            }
        }
        [Authorize(Roles = "3")]
        public ActionResult ClassList()
        {
            c = GetId();
            var classList = cr.ClassList().Where(x => x.TeachUserId == c).ToList();
            //var classList=cr.ClassList(c);
            return View("Class", classList);
        }
        //[Authorize(Roles = "3")]
        //public ActionResult GetClassDataById(int id)
        //{
        //    var rcrd = cr.GetById(id);
        //    return View(rcrd);
        //}

        private int GetId()
        {
            return Convert.ToInt32(Session["id"]);
        }
        //private void GetAllStudents()
        //{
        //    var stdRoll = ur.GetRoll();
        //    var data = (from obj in stdRoll select new { obj.user_id, obj.tbl_Student.FirstOrDefault().std_rollNo });
        //    SelectList getAllRoll = new SelectList(data, "user_id", "std_rollNo");
        //    ViewBag.AllRoll = getAllRoll;

        //}
        //private void GetAllSubjects()
        //{
        //    var subjList = sr.SubjectList();
        //    SelectList getSubj = new SelectList(subjList, "subj_id", "subj_name");
        //    ViewBag.SubjectListName = getSubj;
        //}
    }
}