namespace Bookie.Core.Interfaces
{
    public interface IImporter
    {
        void ProgressCancel();

        void StartScan(string path, bool includeSubdirectories, string searchPattern = "*.*");
    }
}