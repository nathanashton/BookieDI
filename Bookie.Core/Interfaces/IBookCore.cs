using Bookie.Common;
using System.Collections.Generic;

namespace Bookie.Core.Interfaces
{
    public interface IBookCore
    {
        List<Book> GetAllBooks();
    }
}