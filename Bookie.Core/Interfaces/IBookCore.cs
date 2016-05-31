using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Bookie.Core.Interfaces
{
    public interface IBookCore
    {
        ObservableCollection<Common.Entities.Book> GetAllBooks();

        ObservableCollection<Common.Entities.Book> GetAllBooksFromRepository();

        void Persist(Common.Entities.Book book);

        bool Exists(Common.Entities.Book book);

        Common.Entities.Book GetBookById(int id);

        List<Common.Entities.Book> GetBookByTitle(string title);
    }
}