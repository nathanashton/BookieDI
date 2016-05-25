using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Bookie.Common.Entities;

namespace Bookie.Repository.Interfaces
{
    public interface IAuthorRepository : IGenericDataRepository<Author>
    {
        bool Exists(string firstname, string lastname);
        Task<IList<Author>> GetAllAsync(params Expression<Func<Author, object>>[] navigationProperties);
        List<Author> GetAllNested();
    }
}