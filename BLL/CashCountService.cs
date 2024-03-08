using Modle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CashCountService : IService<CashCount>
    {
        CashManagerEntities db = new CashManagerEntities();
        public int Delete(CashCount t)
        {
            db.Entry(t).State = System.Data.Entity.EntityState.Deleted;
            return db.SaveChanges();
        }

        public int Insert(CashCount t)
        {
            db.Entry(t).State = System.Data.Entity.EntityState.Added;
            return db.SaveChanges();
        }
        /// <summary>
        /// 查询所有结果
        /// </summary>
        /// <returns></returns>
        public List<CashCount> Select()
        {
            return db.CashCount.ToList();
        }
        /// <summary>
        /// 单次查询（根据次数ID）
        /// </summary>
        /// <param name="countId">次数ID</param>
        /// <returns></returns>
        public CashCount Select(int countId)
        {
            return db.CashCount.FirstOrDefault(item => item.countID == countId);
        }
        /// <summary>
        /// 查询某一轮的所有结果（根据轮次ID）
        /// </summary>
        /// <param name="roundId">轮次ID</param>
        /// <returns></returns>
        public List<CashCount> Selects(int roundId)
        {
            return db.CashCount.Where(item => item.FK_roundID == roundId).ToList();
        }

        public int Update(CashCount t)
        {
            db.Entry(t).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }
    }
}
