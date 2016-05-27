using Bookie.Common.Entities;
using System.Collections.Generic;

namespace Bookie.Repository.Interfaces
{
    public interface IBookRepository
    {
        IList<Book> GetAll();

        Book GetById(int id);

        List<Book> GetByTitle(string title);

        void Persist(Book book);

        void Remove(Book book);
    }
}