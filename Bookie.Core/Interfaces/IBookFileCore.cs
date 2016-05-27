using Bookie.Common.Entities;

namespace Bookie.Core.Interfaces
{
    public interface IBookFileCore
    {
        bool Exists(BookFile bookFile);
    }
}