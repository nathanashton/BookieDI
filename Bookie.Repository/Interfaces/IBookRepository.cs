using Bookie.Common;
using System.Collections.Generic;

namespace Bookie.Repository.Interfaces
{
    public interface IBookRepository
    {
        List<Book> GetAllBooks();
    }
}