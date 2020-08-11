using AppDB.Db;
using AppModel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDB.DbOperations
{
    public class ClassRepository
    {
        private MODDLEDbContext db;
        public ClassRepository()
        {
            db = new MODDLEDbContext();
        }
        public void Create(ClassViewModel cvm, int c)
        {
            tbl_Class cc = new tbl_Class()
            {
                class_name = cvm.class_name,
                class_code = cvm.class_code,
                TeachUserId = c
            };
            db.tbl_Class.Add(cc);
            SaveChanges();
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }

        public IEnumerable<tbl_Class> ClassList()
        {
            return db.tbl_Class.ToList();
        }

        public ClassViewModel GetById(int id)
        {
            return db.tbl_Class.Where(x => x.class_id == id).Select(
                y => new ClassViewModel()
                {
                    class_id = y.class_id,
                    class_code = y.class_code,
                    class_name = y.class_name,
                    TeachUserId = y.TeachUserId
                }).SingleOrDefault();
        }
    }

}
