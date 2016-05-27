using Bookie.Common.Entities;
using System.Collections.Generic;

namespace Bookie.Repository.Interfaces
{
    public interface IBookFileRepository
    {
        List<BookFile> GetByPath(string fullpath);
    }
}