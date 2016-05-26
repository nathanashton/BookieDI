using System.Collections.Generic;
using Bookie.Common.Entities;

namespace Bookie.Common.Interfaces
{
    public interface IAuthor : IEntity
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string Biography { get; set; }
        string FullName { get; }
        ISet<Book> Books { get; set; }
    }
}