using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modle;

namespace BLL
{
    public class ImageService : IService<CashImage>
    {
        CashManagerEntities db = new CashManagerEntities();
        public int Delete(CashImage t)
        {
            db.Entry(t).State = System.Data.Entity.EntityState.Deleted;
            return db.SaveChanges();
        }

        public int Insert(CashImage t)
        {
          
            db.Entry(t).State = System.Data.Entity.EntityState.Added;
            return db.SaveChanges();
        }

        public List<CashImage> Select()
        {
            return db.CashImage.ToList();
        }

        public CashImage Select(int id)
        {
            return db.CashImage.FirstOrDefault(item =>item.FK_CountID == id);
        }
        public int Update(CashImage t)
        {
            db.Entry(t).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }
    }
}
