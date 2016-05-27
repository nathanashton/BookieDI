using Bookie.Common.Entities;
using Bookie.Repository.Interfaces;
using NHibernate.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Bookie.Repository.Repositories
{
    public class BookFileRepository : IBookFileRepository
    {
        private readonly IDatabase _database;

        public BookFileRepository(IDatabase database)
        {
            _database = database;
        }

        public List<BookFile> GetByPath(string fullpath)
        {
            List<BookFile> bookFiles;
            using (var session = _database.SessionFactory.OpenSession())
            {
                bookFiles = session.Query<BookFile>().Fetch(x => x.Book)
                    .Where(x => x.FullPathAndFileName.Equals(fullpath)).ToList();
            }
            return bookFiles;
        }
    }
}