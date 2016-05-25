using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Bookie.Common;
using Bookie.Common.Entities;
using Bookie.Common.Interfaces;
using Bookie.Repository.Interfaces;

namespace Bookie.Repository.Repositories
{
    public class AuthorRepository : GenericDataRepository<Author>, IAuthorRepository
    {

        public bool Exists(string firstname, string lastname)
        {
            Log.Debug(MethodName.Get());
            using (var context = new SqlCeContext())
            {
                return context.Authors.Any(x => x.FirstName == firstname && x.LastName == lastname);
            }
        }

        public virtual async Task<IList<Author>> GetAllAsync(params Expression<Func<Author, object>>[] navigationProperties)
        {
            Log.Debug(MethodName.Get());
            List<Author> list;
            using (var context = new SqlCeContext())
            {
                IQueryable<Author> dbQuery = context.Set<Author>();
                foreach (var navigationProperty in navigationProperties)
                {
                    dbQuery = dbQuery.Include(navigationProperty);
                }
                list = await dbQuery.AsNoTracking().ToListAsync();
            }

            return list;
        }

        public List<Author> GetAllNested()
        {
            Log.Debug(MethodName.Get());
            using (var ctx = new SqlCeContext())
            {
                return
                    ctx.Authors
                        .AsNoTracking()
                        .ToList();
            }
        }


        public AuthorRepository(ISettings settings, ILog log) : base(settings, log)
        {
        }
    }
}