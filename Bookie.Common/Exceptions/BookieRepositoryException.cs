using System;
using System.Runtime.Serialization;

namespace Bookie.Common.Exceptions
{
    [Serializable]
    public class BookieRepositoryException : Exception
    {
        //private readonly ILog _log;

        public BookieRepositoryException()
        {
            //_log = log;
        }

        public BookieRepositoryException(string message)
            : base(message)
        {
            //_log.Error(message, this);
        }

        public BookieRepositoryException(string format, params object[] args)
            : base(string.Format(format, args))
        {
            //_log.Error("",this);
        }

        public BookieRepositoryException(string message, Exception innerException)
            : base(message, innerException)
        {
            //_log.Error(message, this);
        }

        public BookieRepositoryException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
            //_log.Error("", this);
        }

        protected BookieRepositoryException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            //_log.Error("", this);
        }
    }
}