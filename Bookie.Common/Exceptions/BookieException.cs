﻿using System;
using System.Runtime.Serialization;

namespace Bookie.Common.Exceptions
{
    [Serializable]
    public class BookieException : Exception
    {
        public BookieException()
        {
        }

        public BookieException(string message)
            : base(message)
        {
        }

        public BookieException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

        public BookieException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public BookieException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }

        protected BookieException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}