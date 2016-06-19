using System;
using Bookie.Common.Entities;
using System.Collections.Generic;
using Bookie.Common.EventArgs;

namespace Bookie.Repository.Interfaces
{
    public interface IBookRepository
    {
        IList<Book> GetAll();

        Book GetById(int id);

        List<Book> GetByTitle(string title);

        void Persist(Book book);

        void Remove(Book book);

        event EventHandler<BookEventArgs> BookChanged;
    }
}