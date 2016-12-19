using Bookie.Common;
using Bookie.Common.Entities;
using Bookie.Common.EventArgs;
using Bookie.Common.Interfaces;
using Bookie.Core.Interfaces;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;

namespace Bookie.Core.BookCore
{
    [ImplementPropertyChanged]
    public class BookCore : IBookCore
    {
        private readonly ILog _log;
        private readonly Ctx _ctx;

        public BookCore(ILog log, Ctx ctx)
        {
            _ctx = ctx;
            _log = log;
            _log.Debug(MethodName.Get());
        }

        public event EventHandler<BookEventArgs> BookChanged;

        public ObservableCollection<Book> GetAllBooks()
        {
            _log.Debug(MethodName.Get());
            return new ObservableCollection<Book>(_ctx.Books.ToList());
        }

        public ObservableCollection<Book> GetAllBooksFromRepository()
        {
            _log.Debug(MethodName.Get());
            return new ObservableCollection<Book>(_ctx.Books.ToList());
        }

        public void Persist(Book book)
        {
            if (_ctx.Entry(book).State == EntityState.Detached)
            {
                _ctx.Books.Add(book);
            }
            _ctx.SaveChanges();
            OnBookChanged(new BookEventArgs { Book = book });
            _log.Info("Persisted : " + book.Title);
        }

        public int Exists(Book book)
        {
            return 0;
            //_log.Debug(MethodName.Get());
            //foreach (var file in book.BookFiles)
            //{
            //    if (_bookFileCore.Exists(file))
            //    {
            //        //get book that has this file and return its id
            //        return file.Book.Id;
            //    }
            //}
            //return 0;
        }

        public Book GetBookById(int id)
        {
            return null;
            //_log.Debug(MethodName.Get());
            //return _bookRepository.GetById(id);
        }

        public List<Book> GetBookByTitle(string title)
        {
            return null;
            //_log.Debug(MethodName.Get());
            //return _bookRepository.GetByTitle(title);
        }

        private void OnBookChanged(BookEventArgs e)
        {
            BookChanged?.Invoke(this, e);
        }
    }
}