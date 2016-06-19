using Bookie.Common;
using Bookie.Common.Entities;
using Bookie.Common.Interfaces;
using Bookie.Core.Interfaces;
using Bookie.Repository.Interfaces;
using PropertyChanged;
using System.Collections.ObjectModel;

namespace Bookie.Core.AuthorCore
{
    [ImplementPropertyChanged]
    public class AuthorCore : IAuthorCore
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly ILog _log;
        private ObservableCollection<Author> _authors;

        public AuthorCore(ILog log, IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
            _log = log;
            _log.Debug(MethodName.Get());
            _authors = new ObservableCollection<Author>();
        }

        public ObservableCollection<Author> GetAllAuthors()
        {
            _log.Debug(MethodName.Get());
            //if (_authors != null) return _authors;
            //else
            //{
            return _authors = new ObservableCollection<Author>(_authorRepository.GetAll());
            //  }
        }

        public int Persist(Author author)
        {
            int id;
            var existing = _authorRepository.Get(author.FirstName, author.LastName);
            if (existing != null)
            {
                id = existing.Id;
               // _authorRepository.Update(author);
                return id;
            }
            id = _authorRepository.Persist(author);
            return id;
        }
    }
}