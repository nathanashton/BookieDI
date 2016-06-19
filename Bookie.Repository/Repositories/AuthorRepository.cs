using Bookie.Common.Entities;
using Bookie.Repository.Interfaces;
using NHibernate.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Bookie.Repository.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly IDatabase _database;

        public AuthorRepository(IDatabase database)
        {
            _database = database;
        }

        public IList<Author> GetAll()
        {
            IList<Author> all;
            using (var session = _database.SessionFactory.OpenSession())
            {
                all =
                    session.Query<Author>()
                        .FetchMany(x => x.Books)
                        .ToList();
            }
            return all;
        }

        public Author Get(string firstname, string lastname)
        {
            using (var session = _database.SessionFactory.OpenSession())
            {
                var author2 = session.Query<Author>().FirstOrDefault(x => x.FirstName == firstname && lastname == x.LastName);
                return author2;
            }
        }

        public void Update(Author author)
        {
            using (var session = _database.SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Update(author);
                    transaction.Commit();
                }
            }
        }

        public int Persist(Author author)
        {
            using (var session = _database.SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(author);
                    transaction.Commit();
                }
            }
            return author.Id;
        }
    }
}