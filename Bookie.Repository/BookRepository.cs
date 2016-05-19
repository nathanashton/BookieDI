using Bookie.Common;
using Bookie.Repository.Interfaces;
using System.Collections.Generic;

namespace Bookie.Repository
{
    public class BookRepository : IBookRepository
    {
        public List<Book> GetAllBooks()
        {
            var book = new Book { Title = "Test Book" };
            var books = new List<Book> { book };
            return books;
        }
    }
}