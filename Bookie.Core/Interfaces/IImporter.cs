namespace Bookie.Core.Interfaces
{
    public interface IImporter
    {
        void ProgressCancel();

        void AddBooks(string[] filePaths);

        void AddFromFolder(string path, bool includeSubdirectories, string searchPattern = "*.*");
    }
}