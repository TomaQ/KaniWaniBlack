using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KaniWaniBlack.Data.DAL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll(params Expression<Func<T, object>>[] condition);

        T Get(Func<T, bool> where, params Expression<Func<T, object>>[] condition);

        void Add(params T[] items);

        void Update(params T[] items);

        void Remove(params T[] items);
    }
}