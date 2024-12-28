using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EntityUoW.Data
{
    public class RepositoryBase<TEntity>(DbContext dbContext) : IDisposable where TEntity : class
    {
        #region Constructor

        private readonly DbContext _context = dbContext;

        #endregion Constructor

        #region Disposable

        ~RepositoryBase() => Dispose();

        public void Dispose() => GC.SuppressFinalize(this);

        #endregion Disposable

        #region Sync

        public void Insert(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
        }

        public void BatchInsert(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
            _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void BatchUpdate(IEnumerable<TEntity> entities)
        {
            foreach (TEntity entity in entities)
            {
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void DeleteById(params object[] id)
        {
            TEntity? obj = _context.Set<TEntity>().Find(id);
            if (obj != null)
            {
                _context.Entry(obj).State = EntityState.Deleted;
                _context.Set<TEntity>().Remove(obj);
                _context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }

        public void BatchDelete(IEnumerable<TEntity> entities)
        {
            foreach (TEntity entity in entities)
            {
                _context.Entry(entity).State = EntityState.Deleted;
                _context.Set<TEntity>().Remove(entity);
                _context.SaveChanges();
            }
        }

        public IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> expression) => [.. _context.Set<TEntity>().Where(expression ?? (x => true))];

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression) => _context.Set<TEntity>().Where(expression ?? (x => true));

        public IEnumerable<TEntity> All() => [.. _context.Set<TEntity>()];

        public TEntity? GetById(params object[] id) => _context.Set<TEntity>().Find(id);

        public TEntity? First() => _context.Set<TEntity>().FirstOrDefault();

        public TEntity? First(Expression<Func<TEntity, bool>> expression) => _context.Set<TEntity>().FirstOrDefault(expression ?? (x => true));

        public TEntity? Last() => _context.Set<TEntity>().LastOrDefault();

        public TEntity? Last(Expression<Func<TEntity, bool>> expression) => _context.Set<TEntity>().LastOrDefault(expression ?? (x => true));

        public IEnumerable<TEntity> Some(Expression<Func<TEntity, bool>> expression, int skip, int amount) => [.. _context.Set<TEntity>().Where(expression ?? (x => true)).Skip(skip).Take(amount)];

        public IEnumerable<TEntity> Skip(Expression<Func<TEntity, bool>> expression, int skip) => [.. _context.Set<TEntity>().Where(expression ?? (x => true)).Skip(skip)];

        public IEnumerable<TEntity> Take(Expression<Func<TEntity, bool>> expression, int amount) => [.. _context.Set<TEntity>().Where(expression ?? (x => true)).Take(amount)];

        public IEnumerable<TEntity> Top<TKey>(Expression<Func<TEntity, TKey>> orderExpression, int amount) => [.. _context.Set<TEntity>().OrderByDescending(orderExpression).Take(amount)];

        public IEnumerable<TEntity> Down<TKey>(Expression<Func<TEntity, TKey>> orderExpression, int amount) => [.. _context.Set<TEntity>().OrderBy(orderExpression).Take(amount)];

        public int Count() => _context.Set<TEntity>().Count();

        public int Count(Expression<Func<TEntity, bool>> expression) => _context.Set<TEntity>().Count(expression ?? (x => true));

        public bool Exist(Expression<Func<TEntity, bool>> expression)
        {
            var entity = _context.Set<TEntity>().FirstOrDefault(expression ?? (x => true));
            return entity != null;
        }

        public bool Exist(params object[] id)
        {
            var entity = _context.Set<TEntity>().Find(id);
            return entity != null;
        }

        public bool Any(Expression<Func<TEntity, bool>> expression) => _context.Set<TEntity>().Any(expression ?? (x => true));

        #endregion Sync

    }
}