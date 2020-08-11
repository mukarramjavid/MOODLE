using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppModel.ViewModels;
using AppDB.Db;
using System.Data.SqlClient;

namespace AppDB.DbOperations
{
    public class ResultRepository
    {
        private MODDLEDbContext db;
        public ResultRepository()
        {
            db = new MODDLEDbContext();
        }
        public void SaveChanges()
        {
            db.SaveChanges();
        }


        public void Insert(ResultViewModel rvm, int id)
        {
            tbl_Result result = new tbl_Result()
            {
                std_grades = rvm.std_grades,
                subjID = rvm.subjID,
                userID = rvm.userID,
                TechrUserID = id
            };
            db.tbl_Result.Add(result);
            SaveChanges();
        }

        public IEnumerable<tbl_Result> StdListForParents(int c)
        {
            return db.tbl_Result.SqlQuery("select * from tbl_Result r where r.userID in (select p.ChildUserID from tbl_Parent p where p.UserId=" + c + ")").ToList();
        }
        public IEnumerable<tbl_Result> GetStdList()
        {
            return db.tbl_Result.ToList();
        }

        public ResultViewModel GetStdById(int id)
        {
            return db.tbl_Result.Where(x => x.result_id == id)
                .Select(y => new ResultViewModel()
                {
                    result_id = y.result_id,
                    userID = y.userID,
                    std_grades = y.std_grades,
                    subjID=y.subjID

                }).FirstOrDefault();

        }

        public bool UpdateResult(int result_id, ResultViewModel rvm, int TchrId)
        {
            tbl_Result result = new tbl_Result();
            if (result != null)
            {
                result.result_id = rvm.result_id;
                result.subjID = rvm.subjID;
                result.userID = rvm.userID;
                result.std_grades = rvm.std_grades;
                result.TechrUserID = TchrId;
            }
            db.Entry(result).State = System.Data.Entity.EntityState.Modified;
            SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var result = new tbl_Result
            {
                result_id=id
            };
            db.Entry(result).State = System.Data.Entity.EntityState.Deleted;
            SaveChanges();
            return false;
        }
    }
}
