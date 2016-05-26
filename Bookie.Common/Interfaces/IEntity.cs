using System;

namespace Bookie.Common.Interfaces
{
    public interface IEntity
    {
        int Id { get; set; }
        DateTime? ModifiedDateTime { get; set; }
    }

}