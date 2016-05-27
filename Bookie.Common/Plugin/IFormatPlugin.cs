namespace Bookie.Common.Plugin
{
    public interface IFormatPlugin
    {
        string Format { get; set; }

        void Activate();
    }
}