using Bookie.Common;
using Bookie.Core.Interfaces;
using Bookie.Repository.Interfaces;
using System.Collections.Generic;

namespace Bookie.Core
{
    public class BookCore : IBookCore
    {
        private readonly IBookRepository _bookRepository;

        public BookCore(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public List<Book> GetAllBooks()
        {
            return _bookRepository.GetAllBooks();
        }
    }
}