using Bookie.Common.Entities;
using System.Collections.Generic;

namespace Bookie.Common.Interfaces
{
    public interface IPublisher : IEntity
    {
        string Name { get; set; }
        ISet<Book> Books { get; set; }
    }
}