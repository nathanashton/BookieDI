using Bookie.Common.Entities;
using Bookie.Common.EventArgs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Bookie.Core.Interfaces
{
    public interface IBookCore
    {
        ObservableCollection<Book> GetAllBooks();

        ObservableCollection<Book> GetAllBooksFromRepository();

        void Persist(Book book);

        int Exists(Book book);

        Book GetBookById(int id);

        List<Book> GetBookByTitle(string title);

        event EventHandler<BookEventArgs> BookChanged;
    }
}