using Bookie.Common.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Bookie.Core.Interfaces
{
    public interface IBookCore
    {
        ObservableCollection<Book> GetAllBooks();

        ObservableCollection<Book> GetAllBooksFromRepository();

        void Persist(Book book);

        bool Exists(Book book);

        Book GetBookById(int id);

        List<Book> GetBookByTitle(string title);
    }
}