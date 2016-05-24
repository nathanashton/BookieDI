using System.Data.SQLite;

namespace Bookie.Repository.Interfaces
{
    // ReSharper disable once InconsistentNaming
    public interface ISQLiteRepository
    {
        SQLiteConnection Connection();
        void CreateDatabase();
        void CreateSampleData();
    }
}