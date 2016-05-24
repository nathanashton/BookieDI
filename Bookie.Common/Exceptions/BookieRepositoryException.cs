using System;
using System.Runtime.Serialization;

namespace Bookie.Common.Exceptions
{
    [Serializable]
    public class BookieRepositoryException : Exception
    {
        public BookieRepositoryException()
        {
        }

        public BookieRepositoryException(string message)
            : base(message)
        {
        }

        public BookieRepositoryException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

        public BookieRepositoryException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public BookieRepositoryException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }

        protected BookieRepositoryException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}