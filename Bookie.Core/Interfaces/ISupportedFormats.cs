using System.Collections.ObjectModel;
using Bookie.Common.Plugin;

namespace Bookie.Core.Interfaces
{
    public interface ISupportedFormats
    {
        ObservableCollection<SupportedFormatPlugin> LoadedPlugins { get; set; }
        void LoadFromPath(string path, bool includeSubdirectories = false);
    }
}