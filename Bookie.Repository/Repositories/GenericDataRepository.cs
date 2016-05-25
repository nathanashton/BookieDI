using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlServerCe;
using System.Linq;
using System.Linq.Expressions;
using Bookie.Common;
using Bookie.Common.Exceptions;
using Bookie.Common.Interfaces;
using Bookie.Repository.Interfaces;

namespace Bookie.Repository.Repositories
{
    using EntityState = System.Data.Entity.EntityState;

    public class GenericDataRepository<T> : IGenericDataRepository<T> where T : class, IEntity
    {

        public GenericDataRepository(ISettings settings, ILog log)
        {
            SqlCeContext.Settings = settings;
            Log = log;
            Log.Debug(MethodName.Get());
        }

        public ILog Log { get; set; }

        public virtual IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            Log.Debug(MethodName.Get());
            List<T> list;
            try
            {
                using (var context = new SqlCeContext())
                {
                    IQueryable<T> dbQuery = context.Set<T>();
                    foreach (var navigationProperty in navigationProperties)
                    {
                        dbQuery = dbQuery.Include(navigationProperty);
                    }
                    list = dbQuery.AsNoTracking().ToList();
                }
            }
            catch (SqlCeException ex)
            {
                throw new BookieRepositoryException($"{typeof(T)} - {ex.Message}", ex);
            }
            return list;
        }

        public virtual IList<T> GetList(Func<T, bool> where,params Expression<Func<T, object>>[] navigationProperties)
        {
            Log.Debug(MethodName.Get());
            List<T> list;
            try
            {
                using (var context = new SqlCeContext())
                {
                    IQueryable<T> dbQuery = context.Set<T>();

                    //Apply eager loading
                    foreach (var navigationProperty in navigationProperties)
                    {
                        dbQuery = dbQuery.Include(navigationProperty);
                    }
                    list = dbQuery.AsNoTracking().Where(where).ToList();
                }
            }
            catch (SqlCeException ex)
            {
                throw new BookieRepositoryException($"{typeof(T)} - {ex.Message}", ex);
            }
            return list;
        }

        public virtual T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            Log.Debug(MethodName.Get());
            T item;
            try
            {
                using (var context = new SqlCeContext())
                {
                    IQueryable<T> dbQuery = context.Set<T>();

                    //Apply eager loading
                    foreach (var navigationProperty in navigationProperties)
                    {
                        dbQuery = dbQuery.Include(navigationProperty);
                    }
                    item = dbQuery
                        .AsNoTracking() //Don't track any changes for the selected item
                        .FirstOrDefault(where); //Apply where clause
                }
            }
            catch (SqlCeException ex)
            {
                throw new BookieRepositoryException($"{typeof(T)} - {ex.Message}", ex);
            }
            return item;
        }

        public virtual void Add(params T[] items)
        {
            Log.Debug(MethodName.Get());
            Update(items);
        }

        public virtual void Update(params T[] items)
        {
            Log.Debug(MethodName.Get());
            try
            {
                using (var context = new SqlCeContext())
                {
                    var dbSet = context.Set<T>();
                    foreach (var item in items)
                    {
                        dbSet.Add(item);
                        foreach (var entry in context.ChangeTracker.Entries<IEntity>())
                        {
                            var entity = entry.Entity;
                            entry.State = GetEntityState(entity.EntityState);
                            if (entry.State == EntityState.Added)
                            {
                                entry.Entity.ModifiedDateTime = DateTime.Now;
                            }
                            if (entry.State == EntityState.Modified)
                            {
                                entry.Entity.ModifiedDateTime = DateTime.Now;
                            }
                        }
                    }
                    context.SaveChanges();
                }
            }
            catch (SqlCeException ex)
            {
                throw new BookieRepositoryException($"{typeof(T)} - {ex.Message}", ex);
            }
        }

        public void Remove(params T[] items)
        {
            Log.Debug(MethodName.Get());
            Update(items);
        }

        public void Attach(T entity)
        {
            Log.Debug(MethodName.Get());
            throw new NotImplementedException();
        }

        protected static EntityState GetEntityState(Common.Interfaces.EntityState entityState)
        {
            switch (entityState)
            {
                case Common.Interfaces.EntityState.Unchanged:
                    return EntityState.Unchanged;

                case Common.Interfaces.EntityState.Added:
                    return EntityState.Added;

                case Common.Interfaces.EntityState.Modified:
                    return EntityState.Modified;

                case Common.Interfaces.EntityState.Deleted:
                    return EntityState.Deleted;

                default:
                    return EntityState.Detached;
            }
        }
    }
}