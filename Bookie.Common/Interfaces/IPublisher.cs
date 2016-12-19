using Bookie.Common.Entities;
using System.Collections.Generic;

namespace Bookie.Common.Interfaces
{
    public interface IPublisher : IEntity
    {
        string Name { get; set; }
        ICollection<Book> Books { get; set; }
    }
}