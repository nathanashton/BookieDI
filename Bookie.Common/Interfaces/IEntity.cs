using System;

namespace Bookie.Common.Interfaces
{
    public interface IEntity
    {
        int Id { get; set; }
        DateTime? ModifiedDateTime { get; set; }
        EntityState EntityState { get; set; }

    }
    public enum EntityState
    {
        Unchanged,
        Added,
        Modified,
        Deleted
    }
}