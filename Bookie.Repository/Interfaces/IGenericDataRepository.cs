using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bookie.Common.Interfaces;

namespace Bookie.Repository.Interfaces
{
    public interface IGenericDataRepository<T> where T : class, IEntity
    {
        ILog Log { get; set; }
        IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties);
        IList<T> GetList(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        void Add(params T[] items);
        void Update(params T[] items);
        void Remove(params T[] items);
        void Attach(T entity);
    }
}