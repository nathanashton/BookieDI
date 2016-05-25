using System.Collections.ObjectModel;
using Bookie.Common.Entities;
using Bookie.Common.Interfaces;

namespace Bookie.Core.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBookCore
    {
        /// <summary>
        /// Returns an ObservableCollection of books from the repository.
        /// </summary>
        /// <returns></returns>
        ObservableCollection<IBook> GetAllBooks();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ObservableCollection<IBook> GetAllBooksFromRepository();

        /// <summary>
        /// Adds a root book to the repository and returns it with an ID. Does not add any of the related entities
        /// </summary>
        /// <param name="book"></param>
        /// <returns>A Book object with ID</returns>
        Book AddBook(Book book);

        /// <summary>
        /// Returns if a book already exists in the repository.
        /// </summary>
        /// <param name="book"></param>
        /// <returns>True or False</returns>
        bool Exists(Book book);

        /// <summary>
        /// Returns a book from the repository based on ID. Returns null if nout found
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Book or null</returns>
        Book GetBookById(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        Book GetBookByTitle(string title);

        /// <summary>
        /// Sets the EntityState of all nested objects to Unchanged
        /// </summary>
        /// <param name="book"></param>
        void SetStateUnchanged(Book book);
    }
}