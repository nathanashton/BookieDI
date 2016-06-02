using Bookie.Common.Entities;

namespace Bookie.Repository.Interfaces
{
    public interface IAuthorRepository
    {
        Author Get(string firstname, string lastname);

        int Persist(Author author);
    }
}