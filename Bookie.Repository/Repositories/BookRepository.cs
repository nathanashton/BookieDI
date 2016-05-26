using System.Collections.Generic;
using System.Linq;
using Bookie.Common.Entities;
using Bookie.Repository.Interfaces;
using NHibernate.Linq;

namespace Bookie.Repository.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly IDatabase _database;

        public BookRepository(IDatabase database)
        {
            _database = database;
        }

        public IList<Book> GetAll()
        {
            IList<Book> all;
            using (var session = _database.SessionFactory.OpenSession())
            {
                all =
                    session.Query<Book>()
                        .FetchMany(x => x.Authors)
                        .FetchMany(r => r.Publishers)
                        .FetchMany(x => x.BookFiles)
                        .Fetch(c => c.CoverImage)
                        .ToList();
            }
            return all;
        }

        public Book GetById(int id)
        {
            Book book;
            using (var session = _database.SessionFactory.OpenSession())
            {
                book = session.Query<Book>().FetchMany(x => x.Authors)
                    .FetchMany(r => r.Publishers)
                    .FetchMany(x => x.BookFiles)
                    .Fetch(c => c.CoverImage).FirstOrDefault(x => x.Id == id);
            }
            return book;
        }

        public List<Book> GetByTitle(string title)
        {
            List<Book> books;
            using (var session = _database.SessionFactory.OpenSession())
            {
                books = session.Query<Book>().FetchMany(x => x.Authors)
                    .FetchMany(r => r.Publishers)
                    .FetchMany(x => x.BookFiles)
                    .Fetch(c => c.CoverImage).Where(x => x.Title.Contains(title)).ToList();
            }
            return books;
        }

        public void Persist(Book book)
        {
            using (var session = _database.SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(book);
                    transaction.Commit();
                }
            }
        }

        public void Remove(Book book)
        {
            using (var session = _database.SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Delete(book);
                    transaction.Commit();
                }
            }
        }
    }
}