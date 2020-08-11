using AppDB.Db;
using AppModel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDB.DbOperations
{
    public class SubjectRepository
    {
        private MODDLEDbContext db;
        public SubjectRepository()
        {
            db = new MODDLEDbContext();
        }
        public void SaveChanges()
        {
            db.SaveChanges();
        }
        public void InsertSubject(SubjectViewModel svm)
        {
            tbl_Subject subj = new tbl_Subject()
            {
                subj_name = svm.subj_name,
                subj_hrs = svm.subj_hrs
            };
            db.tbl_Subject.Add(subj);
            SaveChanges();
        }

        public IEnumerable<SubjectViewModel> SubjectList()
        {
            var list = db.tbl_Subject.Select(
                x => new SubjectViewModel()
                {
                    subj_id = x.subj_id,
                    subj_name = x.subj_name,
                    subj_hrs = x.subj_hrs
                }).ToList();
            return list;
        }

        public SubjectViewModel GetById(int id)
        {
            var model = db.tbl_Subject.Where(x => x.subj_id == id).Select(
               y => new SubjectViewModel()
               {
                   subj_id = y.subj_id,
                   subj_name = y.subj_name,
                   subj_hrs = y.subj_hrs

               }).SingleOrDefault();
            return model;
        }

        public bool UpdateSubject(int id, SubjectViewModel svm)
        {
            var subj = new tbl_Subject();
            if (subj != null)
            {
                subj.subj_id = svm.subj_id;
                subj.subj_name = svm.subj_name;
                subj.subj_hrs = svm.subj_hrs;
            }
            db.Entry(subj).State = System.Data.Entity.EntityState.Modified;
            SaveChanges();
            return true;
        }

        public bool DeleteSubject(int id)
        {
            var subj = new tbl_Subject()
            {
                subj_id = id
            };
            db.Entry(subj).State = System.Data.Entity.EntityState.Deleted;
            SaveChanges();
            return false;
        }
        public bool IsExist(SubjectViewModel svm)
        {
            return db.tbl_Subject.Any(x => x.subj_name == svm.subj_name);
        }
       
    }
}
