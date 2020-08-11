using AppDB.Db;
using AppModel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDB.DbOperations
{
    public class ClassParticipentRepository
    {
        private MODDLEDbContext db;
        public ClassParticipentRepository()
        {
            db = new MODDLEDbContext();
        }

        public void InsertParticipent(ClassViewModel cvm,int id)
        {
            tbl_ClassParticipent part = new tbl_ClassParticipent()
            {
                ClassID = cvm.class_id,
                ClassName=cvm.class_name,
                StdUserID = id
            };
            db.tbl_ClassParticipent.Add(part);
            SaveChanges();
        }
        public IEnumerable<tbl_ClassParticipent> GetAllPartiClass()
        {
            return db.tbl_ClassParticipent.ToList();
        }
        private void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}
