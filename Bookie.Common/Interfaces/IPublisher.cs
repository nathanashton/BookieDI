using System.Collections.Generic;
using Bookie.Common.Entities;

namespace Bookie.Common.Interfaces
{
    public interface IPublisher : IEntity
    {
        string Name { get; set; }
        ISet<Book> Books { get; set; }
    }
}