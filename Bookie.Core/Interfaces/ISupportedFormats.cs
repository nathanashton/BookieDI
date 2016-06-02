using Bookie.Common.Plugin;
using System.Collections.ObjectModel;

namespace Bookie.Core.Interfaces
{
    public interface ISupportedFormats
    {
        ObservableCollection<SupportedFormatPluginWrapper> LoadedPlugins { get; set; }

        void LoadFromPath(string path, bool includeSubdirectories = false);
    }
}