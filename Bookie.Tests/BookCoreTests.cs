using Bookie.Common.Entities;
using Bookie.Core.Interfaces;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Bookie.Tests
{
    [TestClass]
    public class BookCoreTests
    {
        private Author _author;
        private Author _author2;
        private Author _author3;
        private Book _book;
        private Book _book2;
        private Book _book3;
        private Publisher _publisher;
        private Publisher _publisher2;
        private Publisher _publisher3;
        private BookFile _bookFile;

        public IBookCore Seed()
        {
            var container = Database.CleanDatabase();
            var core = container.Resolve<IBookCore>();

            _book = new Book { Title = "This is a test book" };
            _author = new Author { FirstName = "Tom", LastName = "Clancy" };
            _publisher = new Publisher { Name = "Wiley Publishing" };
            _book.AddAuthor(_author);
            _book.AddPublisher(_publisher);
            _bookFile = new BookFile { FullPathAndFileName = @"C:\test.pdf" };
            _book.AddBookFile(_bookFile);
            core.Persist(_book);

            _book2 = new Book { Title = "This is another test book" };
            _author2 = new Author { FirstName = "David", LastName = "Jones" };
            _publisher2 = new Publisher { Name = "Microsoft Press" };
            _book2.AddAuthor(_author2);
            _book2.AddPublisher(_publisher2);
            core.Persist(_book2);

            _book3 = new Book { Title = "This is another another test book" };
            _author3 = new Author { FirstName = "David", LastName = "Jones" };
            _publisher3 = new Publisher { Name = "Microsoft Press" };
            _book3.AddAuthor(_author3);
            _book3.AddPublisher(_publisher3);
            core.Persist(_book3);
            return core;
        }

        [TestMethod]
        public void Persist()
        {
            var core = Seed();
            var exists = core.GetBookByTitle("This is a test book");
            Assert.IsNotNull(exists);
            Assert.AreEqual(exists[0].Title, "This is a test book");
            Assert.AreEqual(exists[0].Authors.ToList()[0].FullName, "Clancy, Tom");
            Assert.AreEqual(exists[0].Publishers.ToList()[0].Name, "Wiley Publishing");
        }

        [TestMethod]
        public void GetAllBooks()
        {
            var core = Seed();
            var allBooks = core.GetAllBooks();
            Assert.IsTrue(allBooks.Count == 3);
            Assert.IsTrue(allBooks[0].Authors.ToList().Count == 1);
            Assert.IsTrue(allBooks[1].Authors.ToList()[0].FullName == "Jones, David");
        }

        [TestMethod]
        public void Exists()
        {
            var core = Seed();
            var exists = core.Exists(_book);
            Assert.IsTrue(exists != 0);
        }

        [TestMethod]
        public void GetBookById()
        {
            var core = Seed();
            var bookFromDbUsingId = core.GetBookById(_book.Id);
            Assert.IsTrue(bookFromDbUsingId.Authors.ToList()[0].FullName == "Clancy, Tom");
        }

        [TestMethod]
        public void GetBookByTitle()
        {
            var core = Seed();
            var bookFromDb = core.GetBookByTitle(_book.Title);
            Assert.IsNotNull(bookFromDb);
            Assert.AreEqual(_book.Title, bookFromDb[0].Title);
            Assert.AreEqual(_book.Id, bookFromDb[0].Id);
            Assert.IsTrue(bookFromDb[0].Authors.ToList()[0].FullName == "Clancy, Tom");
        }

        [TestMethod]
        public void PersistExistingAuthor()
        {
            var core = Seed();
            var bookFromDb = core.GetBookByTitle(_book2.Title);
            var bookFromDb2 = core.GetBookByTitle(_book3.Title);
            Assert.AreEqual(bookFromDb[0].Authors.ToList()[0].Id, bookFromDb2[0].Authors.ToList()[0].Id);
            Assert.AreEqual(bookFromDb[0].Authors.ToList()[0].FirstName, bookFromDb2[0].Authors.ToList()[0].FirstName);
            Assert.AreEqual(bookFromDb[0].Authors.ToList()[0].LastName, bookFromDb2[0].Authors.ToList()[0].LastName);
        }

        [TestMethod]
        public void UpdateExistingBook()
        {
            var core = Seed();
            var existing = core.GetBookById(_book.Id);
            _book.Title = "Title has been updated";
            core.Persist(_book);
            var bytitle = core.GetBookByTitle("Title has been updated");
            Assert.IsNotNull(bytitle);
            Assert.AreEqual(existing.Id, bytitle[0].Id);
        }
    }
}