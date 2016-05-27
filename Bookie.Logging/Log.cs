using log4net;
using log4net.Core;
using log4net.Repository.Hierarchy;
using System;
using System.Reflection;
using ILog = Bookie.Common.Interfaces.ILog;

namespace Bookie.Logging
{
    public class Log : ILog
    {
        // ReSharper disable once InconsistentNaming
        private static readonly log4net.ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void SetDebugLevel()
        {
            ((Hierarchy)LogManager.GetRepository()).Root.Level = Level.Debug;
            ((Hierarchy)LogManager.GetRepository()).RaiseConfigurationChanged(EventArgs.Empty);
            log.Info("Logging level set at DEBUG");
        }

        public void SetInfoLevel()
        {
            ((Hierarchy)LogManager.GetRepository()).Root.Level = Level.Info;
            ((Hierarchy)LogManager.GetRepository()).RaiseConfigurationChanged(EventArgs.Empty);
            log.Info("Logging level set at INFO");
        }

        public void Debug(string message)
        {
            log.Debug(message);
        }

        public void Debug(string message, Exception exception)
        {
            log.Debug(message, exception);
        }

        public void Info(string message)
        {
            log.Info(message);
        }

        public void Info(string message, Exception exception)
        {
            log.Info(message, exception);
        }

        public void Error(string message)
        {
            log.Error(message);
        }

        public void Error(string message, Exception exception)
        {
            log.Error(message, exception);
        }
    }
}