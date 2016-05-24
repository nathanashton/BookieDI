namespace Bookie.Common.Interfaces
{
    public interface ISettings
    {
        string ApplicationName { get; }
        string ApplicationPath { get; }
        string DatabasePath { get; }
        string ImageCoversPath { get; }
    }
}