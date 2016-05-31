namespace Bookie.Common.Plugin
{
    public interface ISupportedFormatPlugin
    {
        string Format { get; set; }

        string Activate();

        void ExtractCover(string filePath);
    }
}