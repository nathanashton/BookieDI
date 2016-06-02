using Bookie.Common.Entities;
using Bookie.Repository.Interfaces;
using NHibernate.Linq;
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

        public Author Get(string firstname, string lastname)
        {
            using (var session = _database.SessionFactory.OpenSession())
            {
                var author2 = session.Query<Author>().FirstOrDefault(x => x.FirstName == firstname && lastname == x.LastName);
                return author2;
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