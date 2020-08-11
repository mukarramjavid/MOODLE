using AppDB.DbOperations;
using AppModel.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MDL.Controllers
{
    public class PostsController : Controller
    {
        private PostRepository pr;
        private CommentRepository cr;
        static int? classid;
        static int postid;
        /*get user id from account controller*/
        int c;
        public PostsController()
        {
            cr = new CommentRepository();
            pr = new PostRepository();
        }
        
        /**********************************Manages Posts*************************************************/
        [Authorize(Roles = "3")]
        [HttpGet]
        public ActionResult CreatePost()
        {
            return View();
        }
        [Authorize(Roles = "3")]
        [HttpPost]
        public ActionResult CreatePost(PostViewModel pvm, int id)
        {
            c = GetId();
            pr.CreateNewPost(pvm, id, c);
            return RedirectToAction("CreatePost", "Posts");
        }
        [Authorize(Roles = "2,3")]
        public ActionResult PostDescription(int id)
        {
            var li=GetAll(id);
            return View(li);
        }
        [Authorize(Roles = "2,3")]
        public ActionResult PostById(int id) { 
            var post = pr.GetPostById(id);
            classid = post.classId;
            postid = post.postId;
            return View(post);
        }
        [Authorize(Roles = "2,3")]
        [HttpPost]
        public ActionResult PostById(PostViewModel pvm)
        {
            // User Id
            c = GetId();
            // Data bind with CommentViewModel from PostViewModel
            CommentViewModel cvm = new CommentViewModel();
            cvm.classID = pvm.classId;
            cvm.postID = pvm.postId;
            cvm.comment_text = pvm.text;
            cvm.comment_Time = DateTime.Now.ToString();
            cvm.UserID = c;
            cr.AddComment(cvm);

            return RedirectToAction("PostById", new { id=pvm.postId });
        }
        //[Authorize(Roles = "2,3")]
        public ActionResult GetAllComments()
        {
            return PartialView(cr.GetComments(classid, postid));
        }
        private IEnumerable<PostViewModel> GetAll(int id)
        {
            var list = pr.GetAllPost().Where(x => x.classId == id).ToList();

            List<PostViewModel> li = new List<PostViewModel>();
            foreach (var item in list)
            {
                PostViewModel postList = new PostViewModel();
                postList.postDesc = item.postDesc;
                postList.teacher_name = item.tbl_User.user_name;
                postList.subject_name = item.tbl_Class.class_name;
                postList.classId = item.classId;
                postList.postId = item.postId;
                li.Add(postList);
            }
            return li;
        }
        private int GetId()
        {
            
            return Convert.ToInt32(Session["id"]);
        }
    }
}