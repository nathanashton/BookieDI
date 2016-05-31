using Bookie.Common.Plugin;
using System.Collections.ObjectModel;

namespace Bookie.Core.Interfaces
{
    public interface ISupportedFormats
    {
        ObservableCollection<SupportedFormatPlugin> LoadedPlugins { get; set; }

        void LoadFromPath(string path, bool includeSubdirectories = false);
    }
}