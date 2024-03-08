using Modle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MachineService : IService<Machine>
    {

        CashManagerEntities db = new CashManagerEntities();
        public int Delete(Machine t)
        {
            db.Entry(t).State = System.Data.Entity.EntityState.Deleted;
            return db.SaveChanges();
        }

        public int Insert(Machine t)
        {
            db.Entry(t).State = System.Data.Entity.EntityState.Added;
            return db.SaveChanges();
        }

        public List<Machine> Select()
        {
            return db.Machine.ToList();
        }
        public Machine Select(int id)
        {
            return db.Machine.FirstOrDefault(item => item.machineID == id);
        }
        public int Update(Machine t)
        {
            db.Entry(t).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }
    }
}
