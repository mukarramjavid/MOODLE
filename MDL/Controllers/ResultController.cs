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
    public class ResultController : Controller
    {
        private ResultRepository rr;
        private UserRep ur;
        private SubjectRepository sr;
        /*get user id from account controller*/
        int c;
        public ResultController()
        {
            ur = new UserRep();
            sr = new SubjectRepository();
            rr = new ResultRepository();  
        }
        /**********************************Result*************************************************/
        [Authorize(Roles = "3")]
        [HttpGet]
        public ActionResult Upload_Result()
        {
            GetAllSubjects();
            GetAllStudents();
            return View();
        }
        [Authorize(Roles = "3")]
        [HttpPost]
        public ActionResult Upload_Result(ResultViewModel rvm)
        {
            GetAllSubjects();
            GetAllStudents();
            c = GetId();
            // Get UserId whose result is upoloading
            int Userid = Convert.ToInt32(rvm.userID);
            // Get UserInformation 
            var list = ur.GetAll().Where(x => x.user_id == Userid).ToList();
            // Get Subject 
            var subList = sr.SubjectList().Where(x => x.subj_id == rvm.subjID).ToList();
            // Get ParentUserEmail against above Userid
            var finalId = ur.GetEmail(Userid);
            if (ModelState.IsValid)
            {
                //Send mail to Parents
                string email = finalId.FirstOrDefault().user_email;
                // User Data
                string UserEmail = list.FirstOrDefault().user_email;
                string UserName = list.FirstOrDefault().user_name;
                string UserRollNum = list.FirstOrDefault().tbl_Student.FirstOrDefault().std_rollNo;
                // Get Subject Name
                var name = subList.FirstOrDefault().subj_name;
                // Send Data to Mail Message function
                SendMail(email, rvm, UserEmail, UserName, UserRollNum, name);
                rr.Insert(rvm, c);
                //TempData["IsSuccess"] = "Inserted Successfully";

                TempData["IsSuccess"] = "Inserted Successfully";
            }
            else
            {
                TempData["IsFail"] = "UnSccessfull";
            }
            return RedirectToAction("Upload_Result", "Result");
        }

        // Result Report
        [Authorize(Roles = "3")]
        public ActionResult StdList()
        {
            c = GetId();
            var list = rr.GetStdList().Where(x => x.TechrUserID == c);
            return View(list);

        }
        // Edit Result
        [Authorize(Roles = "3")]
        public ActionResult GetById(int id)
        {
            GetAllSubjects();
            GetAllStudents();
            var rcrd = rr.GetStdById(id);
            return View(rcrd);

        }
        [Authorize(Roles = "3")]
        [HttpPost]
        public ActionResult GetById(ResultViewModel rvm)
        {
            // Teacher Id
            c = GetId();
            GetAllSubjects();
            GetAllStudents();
            if (ModelState.IsValid)
            {
                rr.UpdateResult(rvm.result_id, rvm,c);
            }
            
            return RedirectToAction("StdList", "Result");

        }
        // Delete Result object
        [Authorize(Roles = "3")]
        public ActionResult DeleteResult(int id)
        {
            rr.Delete(id);
            return RedirectToAction("StdList", "Result");
        }

        // Sending Mail To Inform Upload Result
        private void SendMail(string email, ResultViewModel rvm, string UserEmail, string UserName, string UserRoll, string subjName)
        {

            WebMail.Send(email
                        , "**Report Card**"
                        //mail message body
                        , CreateBody(UserEmail, UserName, UserRoll, rvm.std_grades, subjName)
                        , null
                        , UserEmail
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
        private string CreateBody(string email, string name, string roll, string grade, string subjName)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/Views/ReportCard.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{user_email}", email);
            body = body.Replace("{user_name}", name);
            body = body.Replace("{user_roll}", roll);
            body = body.Replace("{user_subj}", subjName);
            body = body.Replace("{user_grade}", grade);

            return body;
        }

        private int GetId()
        {
            return Convert.ToInt32(Session["id"]);
        }
        private void GetAllStudents()
        {
            var stdRoll = ur.GetRoll();
            var data = (from obj in stdRoll select new { obj.user_id, obj.tbl_Student.FirstOrDefault().std_rollNo });
            SelectList getAllRoll = new SelectList(data, "user_id", "std_rollNo");
            ViewBag.AllRoll = getAllRoll;

        }
        private void GetAllSubjects()
        {
            var subjList = sr.SubjectList();
            SelectList getSubj = new SelectList(subjList, "subj_id", "subj_name");
            ViewBag.SubjectListName = getSubj;
        }
    }
}