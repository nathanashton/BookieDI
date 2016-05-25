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
    public class BookRepository : GenericDataRepository<Book>, IBookRepository
    {

        public bool Exists(string filePath)
        {
            Log.Debug(MethodName.Get());
            using (var context = new SqlCeContext())
            {
                return false;
            }
        }

        public virtual async Task<IList<Book>> GetAllAsync(params Expression<Func<Book, object>>[] navigationProperties)
        {
            Log.Debug(MethodName.Get());
            List<Book> list;
            using (var context = new SqlCeContext())
            {
                IQueryable<Book> dbQuery = context.Set<Book>();
                foreach (var navigationProperty in navigationProperties)
                {
                    dbQuery = dbQuery.Include(navigationProperty);
                }
                list = await dbQuery.AsNoTracking().ToListAsync();
            }

            return list;
        }

        public List<Book> GetAllNested()
        {
            Log.Debug(MethodName.Get());
            using (var ctx = new SqlCeContext())
            {
                return
                    ctx.Books
                        .Include(b => b.BookFiles)
                        .Include(c => c.CoverImage)
                        .Include(r => r.Authors)
                        .Include(r => r.Publishers)
                        .AsNoTracking()
                        .ToList();
            }
        }


        public BookRepository(ISettings settings, ILog log) : base(settings, log)
        {
        }
    }
}