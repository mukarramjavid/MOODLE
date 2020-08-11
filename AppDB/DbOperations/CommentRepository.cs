using AppDB.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppModel.ViewModels;

namespace AppDB.DbOperations
{
    public class CommentRepository
    {
        private MODDLEDbContext db;
        public CommentRepository()
        {
            db = new MODDLEDbContext();
        }

        public void AddComment(CommentViewModel cvm)
        {

            tbl_Comment comment = new tbl_Comment()
            {
                classID=cvm.classID,
                UserID=cvm.UserID,
                postID=cvm.postID,
                comment_text=cvm.comment_text,
                comment_Time=cvm.comment_Time
            };
            db.tbl_Comment.Add(comment);
            SaveChanges();
        }
        private void SaveChanges()
        {
            db.SaveChanges();
        }

        public IEnumerable<tbl_Comment> GetComments(int? classid, int postid)
        {
            return db.tbl_Comment.Where(x => x.classID == classid && x.postID == postid).OrderByDescending(x=>x.comment_Time).ToList();
        }
    }
}
