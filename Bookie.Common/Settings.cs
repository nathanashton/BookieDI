using System;
using System.IO;
using Bookie.Common.Interfaces;
using PropertyChanged;

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

        public string ApplicationPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ApplicationName);

        public string DatabasePath => Path.Combine(ApplicationPath, "bookie.sdf");
        public string ImageCoversPath => ApplicationPath + @"\Covers\";
    }
}