using Modle;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RoundService : IService<Round>
    {

        CashManagerEntities db = new CashManagerEntities();
        public int Delete(Round t)
        {
            db.Entry(t).State = System.Data.Entity.EntityState.Deleted;
            return db.SaveChanges();
        }

        public int Insert(Round t)
        {
            db.Entry(t).State = System.Data.Entity.EntityState.Added;
            return db.SaveChanges();
        }

        public List<Round> Select()
        {
            return db.Round.ToList();
        }

        public Round Select(int id)
        {
            return db.Round.FirstOrDefault(item => item.countRoundID == id);
        }
        public int Update(Round t)
        {
            db.Round.AddOrUpdate(t);
            //db.Entry(t).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }
    }
}
