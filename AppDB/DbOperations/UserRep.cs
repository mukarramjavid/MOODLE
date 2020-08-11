using AppDB.Db;
using AppModel;
using AppModel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDB.DbOperations
{
    public class UserRep
    {
        private MODDLEDbContext db;

        public UserRep()
        {
            db = new MODDLEDbContext();
        }

        public tbl_User Login(UserViewModel user)
        {
            return db.tbl_User.Where(x => x.user_email == user.user_email && x.user_pwd == user.user_pwd).SingleOrDefault();
        }

        public String[] GetAllRoles(string username)
        {
            string[] role = { db.tbl_User.Where(x => x.user_email == username).FirstOrDefault().role_id.ToString() };
            return role;
        }

        public IEnumerable<tbl_Role> GetRoles()
        {
            return db.tbl_Role.ToList();
        }

        public void Insert(UserViewModel uvm)
        {
            tbl_User user = new tbl_User()
            {
                user_name = uvm.user_name,
                user_email = uvm.user_email,
                user_pwd = uvm.user_pwd,
                role_id = uvm.role_id

            };
            db.tbl_User.Add(user);
            SaveChanges();
            int userid = user.user_id;

            //StudentViewModel svm = new StudentViewModel();
            if (uvm.role_id == 2)
            {
                tbl_Student stdUser = new tbl_Student()
                {
                    std_rollNo = uvm.student.std_rollNo,
                    std_degree = uvm.student.std_degree,
                    std_deppt = uvm.student.std_deppt,
                    userID = userid
                };
                db.tbl_Student.Add(stdUser);
                SaveChanges();
            }

            if (uvm.role_id == 3)
            {
                tbl_Teacher tchrUser = new tbl_Teacher()
                {
                    teacher_designation = uvm.teacher.teacher_designation,
                    userID = userid
                };
                db.tbl_Teacher.Add(tchrUser);
                SaveChanges();
            }
            if (uvm.role_id == 4)
            {
                tbl_Parent parentUser = new tbl_Parent
                {
                    UserId = userid,
                    ChildUserID = uvm.user_id
                };
                db.tbl_Parent.Add(parentUser);
                SaveChanges();
            }

        }

        //public bool IsRollExist(UserViewModel uvm)
        //{
        //    return db.tbl_Student.Any(x => x.std_rollNo == uvm.tbl_Student.std_rollNo);
        //}

        public bool IsExist(UserViewModel uvm)
        {
            return db.tbl_User.Any(x => x.user_email == uvm.user_email);
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }

        public IEnumerable<tbl_User> GetAll()
        {
            //var list = db.tbl_User.SqlQuery("Select * from tbl_User").ToList();
            var list = db.tbl_User.ToList();
            return list;
        }

        public UserViewModel GetById(int id)
        {
            var rcrd = db.tbl_User
                .Where(x => x.user_id == id)
                .Select(x => new UserViewModel()
                {
                    user_id = x.user_id,
                    user_name = x.user_name,
                    user_email = x.user_email,
                    user_pwd = x.user_pwd,
                    role_id = x.role_id,
                    ImagePath=x.ImagePath
                    
                }).FirstOrDefault();
            return rcrd;
        }

        public bool Update(int id, UserViewModel x)
        {
            var user = new tbl_User();
            if (user != null)
            {
                user.user_id = x.user_id;
                user.user_name = x.user_name;
                user.user_email = x.user_email;
                user.user_pwd = x.user_pwd;
                user.role_id = x.role_id;
                user.ImagePath = x.ImagePath;
            }
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var user = new tbl_User()
            {
                user_id = id
            };

            db.Entry(user).State = System.Data.Entity.EntityState.Deleted;
            SaveChanges();
            return false;
        }
        // Get Parent Email from tbl_Parent
        public IEnumerable<tbl_User> GetEmail(int ChildID)
        {
            return db.tbl_User.SqlQuery("select * from tbl_User u where u.user_id in (select p.UserId from tbl_Parent p where p.ChildUserID=" + ChildID + ")").ToList();

        }
        public IEnumerable<tbl_User> GetRoll()
        {
            return db.tbl_User.SqlQuery("SELECT * FROM tbl_User inner join tbl_Student on tbl_User.user_id=tbl_Student.userID").ToList();

            //return db.tbl_User.Where(x => x.role_id == 2).Where(x=>x.user_id==5042).ToList();
        }
    }

}

