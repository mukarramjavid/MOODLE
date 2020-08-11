using AppDB.DbOperations;
using AppModel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MDL.Controllers
{
    public class ModdleController : Controller
    {
        private ScheduleRepository shr;
        public ModdleController()
        {
            shr = new ScheduleRepository();   
        }
        [Authorize(Roles = "2,3,4")]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "1,2,3,4")]
        public ActionResult ScheduleBySession()
        {
            return View();
            
        }
        [HttpPost]
        [Authorize(Roles = "1,2,3,4")]
        public ActionResult ScheduleBySession(ScheduleViewModel svm)
        {
            var list = shr.ViewBySession(svm);
            return View("ViewSchedule",list);

        }

    }
}