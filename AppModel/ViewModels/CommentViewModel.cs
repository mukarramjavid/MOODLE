using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppModel.ViewModels
{
    public class CommentViewModel
    {
        public string comment_text { get; set; }
        public Nullable<int> UserID { get; set; }
        public string comment_Time { get; set; }
        public Nullable<int> postID { get; set; }
        public Nullable<int> classID { get; set; }
        //public string UserName { set; get; }

        public PostViewModel MyPost { set; get; }

    }
}
