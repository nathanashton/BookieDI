using Bookie.Common.Entities;

namespace Bookie.Core.Interfaces
{
    public interface IAuthorCore
    {
        int Persist(Author author);
    }
}