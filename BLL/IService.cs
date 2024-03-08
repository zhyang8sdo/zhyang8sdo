using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IService<T> where T: class
    {
        /// <summary>
        /// 增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        int Insert(T t);
        /// <summary>
        /// 删
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        int Delete(T t);
        /// <summary>
        /// 改
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        int Update(T t);
        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        List<T> Select();
        /// <summary>
        /// 查询单条（根据主键ID）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Select(int id);
    }
}
