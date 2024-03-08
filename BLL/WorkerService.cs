using Modle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class WorkerService : IService<Worker>
    {
        CashManagerEntities db = new CashManagerEntities();
        public int Delete(Worker t)
        {
            db.Entry(t).State = System.Data.Entity.EntityState.Deleted;
            return db.SaveChanges();
        }

        public int Insert(Worker t)
        {
            db.Entry(t).State = System.Data.Entity.EntityState.Added;
            return db.SaveChanges();
        }

        public List<Worker> Select()
        {
            return db.Worker.ToList();
        }

        public Worker Select(int id)
        {
            return db.Worker.FirstOrDefault(item => item.workerID == id);
        }

        public int Update(Worker t)
        {
            db.Entry(t).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }
        public Worker SelectName(string name)
        {
            return db.Worker.FirstOrDefault(item => item.workerName == name);
        }
    }
}
