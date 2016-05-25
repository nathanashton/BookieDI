using System;
using System.Collections.ObjectModel;
using Bookie.Common.Entities;
using Bookie.Common.Exceptions;
using Bookie.Common.Interfaces;
using Bookie.Core.Interfaces;
using Bookie.Repository.Interfaces;

namespace Bookie.Core
{
    public class BookCore : IBookCore
    {
        private readonly IBookRepository _bookRepository;

        private readonly ILog _log;

        private ObservableCollection<IBook> _books;

        public BookCore(IBookRepository bookRepository, ILog log)
        {
            _bookRepository = bookRepository;
            _log = log;
        }

        public ObservableCollection<IBook> GetAllBooks()
        {
            try
            {
                return _books ?? (_books = new ObservableCollection<IBook>(_bookRepository.GetAll(x=> x.Publishers, x=> x.Authors)));
            }
            catch (BookieRepositoryException ex)
            {
                throw new BookieException("Error retrieving books", ex);
            }
        }

        public void AddBook(BookCore book)
        {
            throw new NotImplementedException();
        }

        public bool Exists(BookCore book)
        {
            throw new NotImplementedException();
        }

        public BookCore GetBookById(int id)
        {
            throw new NotImplementedException();
        }

        public void Add()
        {
            _books.Add(new Book {Title = "test"});
        }
    }
}