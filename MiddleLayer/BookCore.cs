using System.Collections.Generic;
using Common;
using Data;

namespace Core
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