using Bookie.Common.Entities;

namespace Bookie.Common.Interfaces
{
    public interface IBookFile : IEntity
    {
        string FullPathAndFileName { get; set; }
        long FileSize { get; set; }
        string FileName { get; }
        string FileExtension { get; }
        Book Book { get; set; }
    }
}