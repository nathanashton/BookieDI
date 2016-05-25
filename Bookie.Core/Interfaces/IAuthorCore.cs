using System.Collections.ObjectModel;
using Bookie.Common.Entities;
using Bookie.Common.Interfaces;

namespace Bookie.Core.Interfaces
{
    public interface IAuthorCore
    {
        ObservableCollection<IAuthor> GetAllAuthors();
        Author AddAuthor(Author author);
        bool Exists(Author author);
        Author GetAuthorId(int id);
        Author GetAuthorByName(string firstname, string lastname);
    }
}