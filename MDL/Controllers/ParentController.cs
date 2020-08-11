using Microsoft.AspNet.Identity;
using AppDB.DbOperations;
using AppModel;
using AppModel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace MDL.Controllers
{
    public class ParentController : Controller
    {

        private ResultRepository rr;

        public ParentController()
        {
            rr = new ResultRepository();
        }
        [Authorize(Roles=("4"))]
        public ActionResult ParentDashboard()
        {
            return View();
        }
        [Authorize(Roles = "4")]
        public ActionResult View_Result()
        {
            //int id = Convert.ToInt32(User.Identity.GetUserId());
            var c = Convert.ToInt32(Session["id"]);
            var list = rr.StdListForParents(c);
            return View(list);
        }
    }
}