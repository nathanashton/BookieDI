using System.Collections.Generic;
using Bookie.Common.Entities;

namespace Bookie.Repository.Interfaces
{
    public interface IBookRepository
    {
        List<Book> GetAllBooks();
    }
}