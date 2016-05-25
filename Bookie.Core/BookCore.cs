using System;
using System.Collections.ObjectModel;
using Bookie.Common.Entities;
using Bookie.Common.Exceptions;
using Bookie.Common.Interfaces;
using Bookie.Core.Interfaces;
using Bookie.Repository.Interfaces;

namespace Bookie.Core
{
    /// <summary>
    /// Manages core functions for Book objects
    /// </summary>
    public class BookCore : IBookCore
    {
        private readonly IBookRepository _bookRepository;

        private readonly ILog _log;

        private ObservableCollection<IBook> _books;
        /// <summary>
        /// Constructor with DI for BookRepository and Log
        /// </summary>
        /// <param name="bookRepository"></param>
        /// <param name="log"></param>
        public BookCore(IBookRepository bookRepository, ILog log)
        {
            _bookRepository = bookRepository;
            _log = log;
        }

        /// <summary>
        /// Returns an ObservableCollection of books from the repository.
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<IBook> GetAllBooks()
        {
            try
            {
                return _books ?? (_books = new ObservableCollection<IBook>(_bookRepository.GetAll(x=> x.Publishers, x=> x.Authors, x=> x.BookFiles, x=> x.CoverImage)));
            }
            catch (BookieRepositoryException ex)
            {
                throw new BookieException("Error retrieving books", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<IBook> GetAllBooksFromRepository()
        {
            try
            {
                _books = new ObservableCollection<IBook>(_bookRepository.GetAll(x => x.Publishers, x => x.Authors));
                return _books;
            }
            catch (BookieRepositoryException ex)
            {
                throw new BookieException("Error retrieving books", ex);
            }
        }

        /// <summary>
        /// Adds a root book to the repository and returns it with an ID. Does not add any of the related entities
        /// </summary>
        /// <param name="book"></param>
        /// <returns>A Book object with ID</returns>
        public Book AddBook(Book book)
        {
            SetStateUnchanged(book); // Ensure that only the root book object is added
            if (Exists(book))
            {
                return book;
            }
            book.EntityState = EntityState.Added;
            try
            {
                _bookRepository.Add(book);
                _books.Add(book);
                return book;
            }
            catch (BookieRepositoryException ex)
            {
                throw new BookieException("Error adding book", ex);
            }
        }

        /// <summary>
        /// Returns if a book already exists in the repository.
        /// </summary>
        /// <param name="book"></param>
        /// <returns>True or False</returns>
        public bool Exists(Book book)
        {
            if (book.BookFiles.Count == 0)
            {
                return false;
            }
            foreach (var file in book.BookFiles)
            {
                if (_bookRepository.Exists(file.FullPathAndFileName))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns a book from the repository based on ID. Returns null if nout found
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Book or null</returns>
        public Book GetBookById(int id)
        {
            try
            {
                return _bookRepository.GetSingle(x => x.Id == id);
            }
            catch (BookieRepositoryException ex)
            {
                throw new BookieException("Error retrieving book by id", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public Book GetBookByTitle(string title)
        {
            try
            {
                return _bookRepository.GetSingle(x => x.Title == title);
            }
            catch (BookieRepositoryException ex)
            {
                throw new BookieException("Error getting book by title", ex);
            }
        }

        /// <summary>
        /// Sets the EntityState of all nested objects to Unchanged
        /// </summary>
        /// <param name="book"></param>
        public void SetStateUnchanged(Book book)
        {
            foreach (var author in book.Authors)
            {
                author.EntityState = EntityState.Unchanged;
            }
            foreach (var publisher in book.Publishers)
            {
                publisher.EntityState = EntityState.Unchanged;
            }
            foreach (var file in book.BookFiles)
            {
                file.EntityState = EntityState.Unchanged;
            }
            if (book.CoverImage != null)
            {
                book.CoverImage.EntityState = EntityState.Unchanged;
            }
        }
    }
}