using Bookie.Common;
using Bookie.Common.Entities;
using Bookie.Common.Interfaces;
using Bookie.Core.Interfaces;
using Bookie.Repository.Interfaces;
using PropertyChanged;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Bookie.Core
{
    [ImplementPropertyChanged]
    public class BookCore : IBookCore
    {
        private readonly IBookFileCore _bookFileCore;
        private readonly IBookRepository _bookRepository;

        private readonly ILog _log;

        private ObservableCollection<Book> _books;

        public BookCore(IBookRepository bookRepository, IBookFileCore bookFileCore, ILog log)
        {
            _bookRepository = bookRepository;
            _bookFileCore = bookFileCore;
            _log = log;
            _log.Debug(MethodName.Get());
            _books = new ObservableCollection<Book>();
        }

        public ObservableCollection<Book> GetAllBooks()
        {
            _log.Debug(MethodName.Get());
            return _books ?? (_books = new ObservableCollection<Book>(_bookRepository.GetAll()));
        }

        public ObservableCollection<Book> GetAllBooksFromRepository()
        {
            _log.Debug(MethodName.Get());
            _books = new ObservableCollection<Book>(_bookRepository.GetAll());
            return _books;
        }

        public void Persist(Book book)
        {
            _log.Debug(MethodName.Get());
            if (book == null) return;
            if (Exists(book)) return;
            _bookRepository.Persist(book);
            _log.Info("Persisted " + book.Title);

            //var toremove = _books.FirstOrDefault(x => x.Id == book.Id);
            //if (toremove != null)
            //{
            //    _books.Remove(toremove);
            //}
            //_books.Add(book);
        }

        public bool Exists(Book book)
        {
            _log.Debug(MethodName.Get());
            var exists = false;
            foreach (var file in book.BookFiles)
            {
                exists = _bookFileCore.Exists(file);
            }
            return exists;
        }

        public Book GetBookById(int id)
        {
            _log.Debug(MethodName.Get());
            return _bookRepository.GetById(id);
        }

        public List<Book> GetBookByTitle(string title)
        {
            _log.Debug(MethodName.Get());
            return _bookRepository.GetByTitle(title);
        }
    }
}