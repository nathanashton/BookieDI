using System.Collections.Generic;
using Common;

namespace Data
{
    public interface IBookRepository
    {
        List<Book> GetAllBooks();
    }
}