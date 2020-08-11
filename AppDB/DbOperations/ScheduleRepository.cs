using AppDB.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppModel.ViewModels;

namespace AppDB.DbOperations
{
    public class ScheduleRepository
    {
        private MODDLEDbContext db;
        public ScheduleRepository()
        {
            db = new MODDLEDbContext();
        }
        public void SaveChanges()
        {
            db.SaveChanges();
        }

        public void Insert(ScheduleViewModel svm)
        {
            tbl_Schedule schd = new tbl_Schedule()
            {
                subjectName = svm.subjectName,
                room = svm.room,
                date = svm.date,
                session = svm.session,
                section = svm.section,
                teacher = svm.teacher,
                deppt=svm.deppt,
                time=svm.time
            };
            db.tbl_Schedule.Add(schd);
            SaveChanges();
        }

        public IEnumerable<tbl_Schedule> ScheduleList()
        {
            return db.tbl_Schedule.ToList();

        }

        public IEnumerable<tbl_Schedule> ViewBySession(ScheduleViewModel svm)
        {
            return db.tbl_Schedule.Where(x => x.session == svm.session && x.deppt==svm.deppt && x.section == svm.section).ToList();
          
        }

        public ScheduleViewModel GetById(int id)
        {
            var model = db.tbl_Schedule.Where(x => x.sheduleId == id).Select(
               y => new ScheduleViewModel()
               {
                   sheduleId=y.sheduleId,
                   section=y.section,
                   session=y.session,
                   deppt=y.deppt,
                   time=y.time,
                   teacher=y.teacher,
                   date=y.date,
                   subjectName=y.subjectName,
                   room=y.room

               }).SingleOrDefault();
            return model;
        }

        public bool UpdateSchedule(int sheduleId, ScheduleViewModel svm)
        {
            var sch = new tbl_Schedule();
            if (sch != null)
            {
                sch.sheduleId = svm.sheduleId;
                sch.subjectName = svm.subjectName;
                sch.teacher = svm.teacher;
                sch.time = svm.time;
                sch.session = svm.session;
                sch.section = svm.section;
                sch.room = svm.room;
                sch.date = svm.date;
                sch.deppt = svm.deppt;
            }
            db.Entry(sch).State = System.Data.Entity.EntityState.Modified;
            SaveChanges();
            return true;
        }

        public bool DeleteSchdule(int id)
        {
            var sch = new tbl_Schedule()
            {
                sheduleId = id
            };
            db.Entry(sch).State = System.Data.Entity.EntityState.Deleted;
            SaveChanges();
            return false;
        }

        public bool IsExist(ScheduleViewModel svm)
        {
            return db.tbl_Schedule.Any(x => x.deppt == svm.deppt && x.session == svm.session && x.time == svm.time);
        }
    }
}