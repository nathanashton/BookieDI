using System.Collections.Generic;
using System.Data.Entity;
using Bookie.Common.Entities;
using Bookie.Common.Interfaces;

namespace Bookie.Repository
{
    public class BookieDbInitialiser : DropCreateDatabaseAlways<SqlCeContext>
    {
        protected override void Seed(SqlCeContext context)
        {
            IList<Book> books = new List<Book>();
            var book = new Book {Title = "Moby Dick", Abstract="A Thrilling tale of adventure on the high seas", Isbn10="123456", Pages=512};
            book.Authors.Add(new Author { FirstName = "Tom", LastName = "Clancy" });
            book.Publishers.Add(new Publisher {Name = "Acme Books"});
            book.BookFiles.Add(new BookFile {FullPathAndFileName = @"C:\books\mobydick.pdf"});
            book.BookFiles.Add(new BookFile { FullPathAndFileName = @"C:\books\mobydick.epub" });
            book.CoverImage = new CoverImage { FullPathAndFileName = @"C:\books\covers\mobydick.png"};
            books.Add(book);

            foreach (var b in books)
            {
                context.Books.Add(b);
            }

            base.Seed(context);
        }
    }
}
