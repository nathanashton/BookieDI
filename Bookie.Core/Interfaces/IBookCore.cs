using System.Collections.ObjectModel;
using Bookie.Common.Interfaces;

namespace Bookie.Core.Interfaces
{
    public interface IBookCore
    {
        ObservableCollection<IBook> GetAllBooks();
        void AddBook(BookCore book);
        bool Exists(BookCore book);
        BookCore GetBookById(int id);
    }
}