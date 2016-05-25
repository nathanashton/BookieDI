using System;
using System.Collections.ObjectModel;
using Bookie.Common.Entities;
using Bookie.Common.Exceptions;
using Bookie.Common.Interfaces;
using Bookie.Core.Interfaces;
using Bookie.Repository.Interfaces;

namespace Bookie.Core
{
    public class AuthorCore : IAuthorCore
    {
        private readonly IAuthorRepository _authorRepository;

        private readonly ILog _log;

        private ObservableCollection<IAuthor> _authors;

        public AuthorCore(IAuthorRepository authorRepository, ILog log)
        {
            _authorRepository = authorRepository;
            _log = log;
        }


        public ObservableCollection<IAuthor> GetAllAuthors()
        {
            try
            {
                return _authors ?? (_authors = new ObservableCollection<IAuthor>(_authorRepository.GetAll()));
            }
            catch (BookieRepositoryException ex)
            {
                throw new BookieException("Error retrieving authors", ex);
            }
        }

        public Author AddAuthor(Author author)
        {
            if (_authorRepository.Exists(author.FirstName, author.LastName))
            {
                author = GetAuthorByName(author.FirstName, author.LastName);
                return author;
            }
            author.EntityState = EntityState.Added;
            _authorRepository.Add(author);
            return author;
        }

        public bool Exists(Author author)
        {
            return _authorRepository.Exists(author.FirstName, author.LastName);
        }

        public Author GetAuthorId(int id)
        {
            throw new NotImplementedException();
        }

        public Author GetAuthorByName(string firstname, string lastname)
        {
            return _authorRepository.GetSingle(x => x.FirstName == firstname && x.LastName == lastname);
        }
    }
}