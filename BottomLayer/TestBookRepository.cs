using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Data
{
    public class TestBookRepository : IBookRepository
    {
        public List<Book> GetAllBooks()
        {
            var book = new Book { Title = "Test Book" };
            var books = new List<Book> { book };
            return books;
        }
    }
}
