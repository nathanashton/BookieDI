using System.Drawing;
using System.Net.Mime;
using System.Windows.Media.Imaging;

namespace Bookie.Common.Plugin
{
    public interface ISupportedFormatPlugin
    {
        string Format { get; set; }

        string Activate();

       void ExtractCover(string filePath);
    }
}