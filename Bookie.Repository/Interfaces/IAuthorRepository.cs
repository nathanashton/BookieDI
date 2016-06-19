using Bookie.Common.Entities;
using System.Collections.Generic;

namespace Bookie.Repository.Interfaces
{
    public interface IAuthorRepository
    {
        IList<Author> GetAll();

        Author Get(string firstname, string lastname);

        int Persist(Author author);

        void Update(Author author);
    }
}