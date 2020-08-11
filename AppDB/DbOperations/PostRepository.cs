using AppDB.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppModel.ViewModels;

namespace AppDB.DbOperations
{
    public class PostRepository
    {
        private MODDLEDbContext db;
        public PostRepository()
        {
            db = new MODDLEDbContext();
        }

        public void CreateNewPost(PostViewModel pvm, int id,int tui)
        {
            tbl_Post post = new tbl_Post()
            {
                postDesc=pvm.postDesc,
                classId=id,
                Tchr_User_Id=tui
            };
            db.tbl_Post.Add(post);
            SaveChanges();
        }

        public IEnumerable<tbl_Post> GetAllPost()
        {
            return db.tbl_Post.ToList();
        }
        public PostViewModel GetPostById(int id)
        {
            return db.tbl_Post.Where(x => x.postId == id)
                .Select(y => new PostViewModel()
                {
                    postId = y.postId,
                    postDesc = y.postDesc,
                    Tchr_User_Id = y.Tchr_User_Id,
                    classId = y.classId,
                    subject_name = y.tbl_Class.class_name,
                    teacher_name=y.tbl_User.user_name
                    

                }).FirstOrDefault();
        }
        private void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}
