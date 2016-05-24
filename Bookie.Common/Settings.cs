using System;
using System.IO;
using Bookie.Common.Interfaces;
using PropertyChanged;

namespace Bookie.Common
{
    [ImplementPropertyChanged]
    public class Settings : ISettings
    {
        public string ApplicationName => "Bookie";

        public string ApplicationPath
            => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ApplicationName);

        public string DatabasePath => Path.Combine(ApplicationPath, "bookie.db");
        public string ImageCoversPath => ApplicationPath + @"\Covers\";
    }
}