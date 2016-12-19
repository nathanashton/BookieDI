using System;
using Bookie.Common.Entities;
using System.Collections.ObjectModel;
using Bookie.Common.EventArgs;

namespace Bookie.Core.Interfaces
{
    public interface IAuthorCore
    {
        ObservableCollection<Author> GetAllAuthors();

        int Persist(Author author);

        event EventHandler<AuthorEventArgs> AuthorChanged;

    }
}