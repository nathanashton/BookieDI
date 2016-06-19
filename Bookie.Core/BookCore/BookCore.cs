using System;
using Bookie.Common;
using Bookie.Common.Entities;
using Bookie.Common.EventArgs;
using Bookie.Common.Interfaces;
using Bookie.Core.Interfaces;
using Bookie.Repository.Interfaces;
using PropertyChanged;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Threading;

namespace Bookie.Core.BookCore
{
    [ImplementPropertyChanged]
    public class BookCore : IBookCore
    {
        private readonly IBookFileCore _bookFileCore;
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorCore _authorCore;
        private readonly ILog _log;

        public event EventHandler<BookEventArgs> BookChanged;

        private ObservableCollection<Book> _books;

        public BookCore(IBookRepository bookRepository, IBookFileCore bookFileCore, ILog log, IAuthorCore authorCore)
        {
            _authorCore = authorCore;
            _bookRepository = bookRepository;
            _bookRepository.BookChanged += _bookRepository_BookChanged;

            _bookFileCore = bookFileCore;
            _log = log;
            _log.Debug(MethodName.Get());
            _books = new ObservableCollection<Book>();
        }

        private void _bookRepository_BookChanged(object sender, BookEventArgs e)
        {
            if (BookChanged != null)
            {
                BookChanged(this, e);
            }
        }

        public ObservableCollection<Book> GetAllBooks()
        {
            _log.Debug(MethodName.Get());
            if (_books == null)
            {
                return new ObservableCollection<Book>(_bookRepository.GetAll());
            }
            return _books;
        }

        public ObservableCollection<Book> GetAllBooksFromRepository()
        {
            _log.Debug(MethodName.Get());
            return new ObservableCollection<Book>(_bookRepository.GetAll());
        }

        public void Persist(Book book)
        {
            _log.Debug(MethodName.Get());
            if (book == null) return;

            var existingBookId = Exists(book);
            if (existingBookId != 0)
            {
                book.Id = existingBookId;
            }

            if (book.Authors != null && book.Authors.Count > 0)
            {
                for (var i = 0; i < book.Authors.Count; i++)
                {
                    var author = book.Authors.ToList()[i];
                    author.Id = _authorCore.Persist(author);
                }
            }

            _bookRepository.Persist(book);
            _log.Info("Persisted : " + book.Title);
        }

        public int Exists(Book book)
        {
            _log.Debug(MethodName.Get());
            foreach (var file in book.BookFiles)
            {
                if (_bookFileCore.Exists(file))
                {
                    //get book that has this file and return its id
                    return file.Book.Id;
                }
            }
            return 0;
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