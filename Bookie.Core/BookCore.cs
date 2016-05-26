using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Bookie.Common.Entities;
using Bookie.Common.Interfaces;
using Bookie.Core.Interfaces;
using Bookie.Repository.Interfaces;
using PropertyChanged;

namespace Bookie.Core
{
    [ImplementPropertyChanged]
    public class BookCore : IBookCore
    {
        private readonly IBookRepository _bookRepository;

        private readonly ILog _log;

        private ObservableCollection<Book> _books;

        public BookCore(IBookRepository bookRepository, ILog log)
        {
            _bookRepository = bookRepository;
            _log = log;
            _books = new ObservableCollection<Book>();
        }
       
        public ObservableCollection<Book> GetAllBooks()
        {
            return _books ?? (_books = new ObservableCollection<Book>(_bookRepository.GetAll()));
        }

        public ObservableCollection<Book> GetAllBooksFromRepository()
        {
            _books = new ObservableCollection<Book>(_bookRepository.GetAll());
            return _books;
        }

        public void Persist(Book book)
        {
            if (book == null) return;
            _bookRepository.Persist(book);

            var toremove = _books.FirstOrDefault(x => x.Id == book.Id);
            if (toremove != null)
            {
                _books.Remove(toremove);
            }
           _books.Add(book);
        }

        public bool Exists(Book book)
        {
            var existing = _bookRepository.GetByTitle(book.Title);
            return existing != null;
        }

        public Book GetBookById(int id)
        {
            return _bookRepository.GetById(id);
        }
    
        public List<Book> GetBookByTitle(string title)
        {
            return _bookRepository.GetByTitle(title);
        }
    }
}