using System.Drawing;

namespace Bookie.Common.Plugin
{
    public interface ISupportedFormatPlugin
    {
        string Format { get; set; }
        string FileExtension { get; set; }

        Image ExtractCover(string inputPath);

        Metadata ExtractMetadata(string inputPath);
    }
}