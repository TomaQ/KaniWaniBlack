using KaniWaniBlack.Data.DAL.Interfaces;
using KaniWaniBlack.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KaniWaniBlack.Data.DAL
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        internal KaniWaniBlackContext _context;

        public GenericRepository(KaniWaniBlackContext aContext)
        {
            _context = aContext;
        }

        public virtual void Add(params T[] items)
        {
            foreach (T item in items)
            {
                _context.Entry(item).State = EntityState.Added;
            }
            _context.SaveChanges();
        }

        public virtual T Get(Func<T, bool> where, params Expression<Func<T, object>>[] conditions)
        {
            IQueryable<T> dbQuery = _context.Set<T>();

            foreach (Expression<Func<T, object>> con in conditions)
            {
                dbQuery = dbQuery.Include(con);
            }

            return dbQuery.FirstOrDefault(where);
        }

        public virtual IQueryable<T> GetAll(params Expression<Func<T, object>>[] conditions)
        {
            IQueryable<T> dbQuery = _context.Set<T>();

            foreach (Expression<Func<T, object>> con in conditions)
            {
                dbQuery = dbQuery.Include<T, object>(con);
            }

            return dbQuery;
        }

        public virtual void Remove(params T[] items)
        {
            foreach (T item in items)
            {
                _context.Entry(item).State = EntityState.Deleted;
            }
            _context.SaveChanges();
        }

        public virtual void Update(params T[] items)
        {
            foreach (T item in items)
            {
                _context.Entry(item).State = EntityState.Modified;
            }
            _context.SaveChanges();
        }
    }
}