using Bookie.Common.Interfaces;
using PropertyChanged;
using System;
using System.IO;

namespace Bookie.Common
{
    [ImplementPropertyChanged]
    public class Settings : ISettings
    {
        public Settings(ILog log)
        {
            log.SetInfoLevel();
        }

        public string ApplicationName => "Bookie";

        public string ApplicationPath
            => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ApplicationName);

        public string DatabasePath => Path.Combine(ApplicationPath, "bookie.db");

        public string ImageCoversPath
        {
            get
            {
                var path = ApplicationPath + @"\Covers\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return path;
            }
        }
    }
}