using Bookie.Common;
using Bookie.Common.Entities;
using Bookie.Common.Interfaces;
using Bookie.Core.Interfaces;
using Bookie.Repository.Interfaces;
using PropertyChanged;

namespace Bookie.Core.AuthorCore
{
    [ImplementPropertyChanged]
    public class AuthorCore : IAuthorCore
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly ILog _log;

        public AuthorCore(ILog log, IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
            _log = log;
            _log.Debug(MethodName.Get());
        }

        public int Persist(Author author)
        {
            int id;
            var existing = _authorRepository.Get(author.FirstName, author.LastName);
            if (existing != null)
            {
                id = existing.Id;
                return id;
            }
            id = _authorRepository.Persist(author);
            return id;
        }
    }
}