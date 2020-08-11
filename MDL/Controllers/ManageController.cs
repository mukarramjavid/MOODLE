using AppDB.DbOperations;
using AppModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MDL.Controllers
{
    public class ManageController : Controller
    {
        private UserRep ur;
        public ManageController()
        {
            ur = new UserRep();
        }
  
        [Authorize(Roles ="1,2,3,4")]
        [HttpGet]
        public ActionResult ProfileData()
        {
            int id = GetId();
            var rcrd = ur.GetById(id);
            return View(rcrd);
        }
        [Authorize(Roles = "1,2,3,4")]
        [HttpGet]
        public ActionResult EditDataById()
        {
            int id = GetId();
            var rcrd = ur.GetById(id);
            if (rcrd.ImagePath == null)
            {
                rcrd.ImagePath = "~/images/no_image_available_male.png";
            }
            return View(rcrd);
        }

        [Authorize(Roles = "1,2,3,4")]
        [HttpPost]
        public ActionResult EditDataById(UserViewModel uvm)
        {
            if (uvm.UserImageFile != null)
            {
                //Get Imagewithout extension
                string fileName = Path.GetFileNameWithoutExtension(uvm.UserImageFile.FileName);
                //Get Extension of file
                string fileExtension = Path.GetExtension(uvm.UserImageFile.FileName);
                //assign unique code to file
                fileName = fileName + DateTime.Now.ToString("yymmssff") + fileExtension;

                //Add path to DB
                uvm.ImagePath = "~/images/" + fileName;
                //Add image to the Project(MDL Layer) folder
                fileName = Path.Combine(Server.MapPath("~/images/"), fileName);
                uvm.UserImageFile.SaveAs(fileName);
            }
            if(uvm.ImagePath== "/images/no_image_available_male.png")
            {
                uvm.ImagePath = null;
            }

            if (ModelState.IsValid)
            {
                ur.Update(uvm.user_id, uvm);
                return RedirectToAction("ProfileData", "Manage");
            }return View();
        }

        private int GetId()
        {
            return Convert.ToInt32(Session["id"]);
        }
    }
}