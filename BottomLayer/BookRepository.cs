using System.Collections.Generic;
using Common;

namespace Data
{
    public class BookRepository : IBookRepository
    {
        public List<Book> GetAllBooks()
        {
            var book = new Book {Title = "Test Book"};
            var books = new List<Book> {book};
            return books;
        }
    }
}