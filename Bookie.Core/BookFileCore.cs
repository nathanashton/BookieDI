using Bookie.Common;
using Bookie.Common.Entities;
using Bookie.Common.Interfaces;
using Bookie.Core.Interfaces;
using Bookie.Repository.Interfaces;

namespace Bookie.Core
{
    public class BookFileCore : IBookFileCore
    {
        private readonly IBookFileRepository _bookFileRepository;

        private readonly ILog _log;

        public BookFileCore(IBookFileRepository bookFileRepository, ILog log)
        {
            _bookFileRepository = bookFileRepository;
            _log = log;
            _log.Debug(MethodName.Get());
        }

        public bool Exists(BookFile bookFile)
        {
            _log.Debug(MethodName.Get());
            var foundBookFiles = _bookFileRepository.GetByPath(bookFile.FullPathAndFileName);
            return foundBookFiles?.Count > 0;
        }
    }
}