using System;

namespace Bookie.Common.Interfaces
{
    public interface ILog
    {
        void Debug(string message);

        void Debug(string message, Exception exception);

        void Info(string message);

        void Info(string message, Exception exception);

        void Error(string message);

        void Error(string message, Exception exception);

        void SetDebugLevel();

        void SetInfoLevel();
    }
}