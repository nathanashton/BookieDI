using Bookie.Common.Entities;
using System.Collections.ObjectModel;

namespace Bookie.Core.Interfaces
{
    public interface IAuthorCore
    {
        ObservableCollection<Author> GetAllAuthors();

        int Persist(Author author);
    }
}