using System.Data.SQLite;
using System.IO;
using Bookie.Common.Interfaces;
using Bookie.Repository.Interfaces;

namespace Bookie.Repository
{
    // ReSharper disable once InconsistentNaming
    public class SQLiteRepository : ISQLiteRepository
    {
        private readonly ILog _log;
        private readonly ISettings _settings;

        public SQLiteRepository(ISettings settings, ILog log)
        {
            _settings = settings;
            _log = log;
        }

        public SQLiteConnection Connection()
        {
            if (!File.Exists(_settings.DatabasePath))
            {
                CreateDatabase();
                CreateSampleData();
            }
            return new SQLiteConnection(@"Data Source=" + _settings.DatabasePath);
        }

        public void CreateDatabase()
        {
            using (
                var connection =
                    new SQLiteConnection(@"Data Source=" + _settings.DatabasePath))
            {
                var sql = @"CREATE TABLE [Authors] (
	                        [Id] integer NOT NULL PRIMARY KEY AUTOINCREMENT, 
	                        [FirstName] nvarchar(254), 
	                        [LastName] nvarchar(254), 
                            [Biography] nvarchar(2000), 
	                        [ModifiedDateTime] timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
                        );
                        CREATE TABLE [BookAuthors] (
	                        [Id] integer NOT NULL PRIMARY KEY AUTOINCREMENT, 
	                        [BookId] integer NOT NULL, 
	                        [AuthorId] integer NOT NULL
                        );
                        CREATE TABLE [BookBookFiles] (
	                        [Id] integer NOT NULL PRIMARY KEY AUTOINCREMENT, 
	                        [BookId] integer NOT NULL, 
	                        [BookFileId] integer NOT NULL
                        );
                        CREATE TABLE [BookFiles] (
	                        [Id] integer NOT NULL PRIMARY KEY AUTOINCREMENT, 
	                        [FullPathAndFileName] nvarchar(500), 
	                        [FileSize] long, 
	                        [ModifiedDateTime] timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
                        );
                        CREATE TABLE [BookPublishers] (
	                        [Id] integer NOT NULL PRIMARY KEY AUTOINCREMENT, 
	                        [BookId] integer NOT NULL, 
	                        [PublisherId] integer NOT NULL
                        );
                        CREATE TABLE [Books] (
	                        [Id] integer NOT NULL PRIMARY KEY AUTOINCREMENT, 
	                        [Title] nvarchar(500), 
	                        [Abstract] nvarchar(2000), 
	                        [DatePublished] datetime, 
	                        [Pages] integer, 
	                        [Isbn10] nvarchar(25), 
	                        [Isbn13] nvarchar(25), 
	                        [Favourite] boolean, 
	                        [Rating] integer, 
	                        [ModifiedDateTime] timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP, 
	                        [CoverFileId] integer
                        );
                        CREATE TABLE [CoverFiles] (
	                        [Id] integer NOT NULL PRIMARY KEY AUTOINCREMENT, 
	                        [FullPathAndFileName] nvarchar(500), 
	                        [FileSize] long, 
	                        [ModifiedDateTime] timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP, 
	                        [BookId] integer
                        );
                        CREATE TABLE [Publishers] (
	                        [Id] integer NOT NULL PRIMARY KEY AUTOINCREMENT, 
	                        [Name] nvarchar(500), 
	                        [ModifiedDateTime] timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
                        );
                        CREATE TABLE [Logs] (
	                        [Id] integer NOT NULL PRIMARY KEY AUTOINCREMENT, 
	                        [Date] datetime, 
	                        [Level] nvarchar(50), 
	                        [Message] nvarchar(200), 
	                        [Exception] nvarchar(2500)
                        );
                        CREATE TABLE [SavedDevices] (
	                        [Id] integer NOT NULL PRIMARY KEY AUTOINCREMENT, 
	                        [SerialNumber] nvarchar(254), 
	                        [Alias] nvarchar(254), 
	                        [SupportedDeviceId] integer
                        );
                        CREATE TABLE [SupportedDevices] (
	                        [Id] integer NOT NULL PRIMARY KEY AUTOINCREMENT, 
	                        [Name] nvarchar(254), 
	                        [Description] nvarchar(2000), 
	                        [Icon] nvarchar(500), 
	                        [DirectoryForFiles] nvarchar(254)
                        );";

                var command = new SQLiteCommand(sql, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                _log.Info("SQLite Database created at " + _settings.DatabasePath);
            }
        }

        public void CreateSampleData()
        {
            using (var conn = Connection())
            {
                var sql = @"INSERT INTO [Books] ([Title],[Abstract],[DatePublished],[Pages],[Favourite],[ModifiedDateTime]) VALUES ('Moby Dick','Classic tale about a whale','2016-05-24 09:34:41.3913972',351,0,'2016-05-23 23:34:49');
INSERT INTO[Books] ([Title],[Abstract],[Favourite],[ModifiedDateTime]) VALUES('Pride & Prejudice','Never read it before',0,'2016-05-23 23:34:59');
        INSERT INTO[BookFiles] ([FullPathAndFileName],[FileSize],[ModifiedDateTime]) VALUES('C:\temp\MobyDick.pdf',12345,'2016-05-23 23:35:40');
        INSERT INTO[BookFiles] ([FullPathAndFileName],[ModifiedDateTime]) VALUES('c:\temp\pandp.mobi','2016-05-23 23:35:49');
        INSERT INTO[BookFiles] ([FullPathAndFileName],[ModifiedDateTime]) VALUES('c:\temp\MobiDick.epub','2016-05-23 23:36:16');
        INSERT INTO[BookBookFiles] ([BookId],[BookFileId]) VALUES(1,1);
        INSERT INTO[BookBookFiles] ([BookId],[BookFileId]) VALUES(2,2);
        INSERT INTO[BookBookFiles] ([BookId],[BookFileId]) VALUES(1,3);
        INSERT INTO[BookAuthors] ([BookId],[AuthorId]) VALUES(1,1);
        INSERT INTO[Authors] ([FirstName],[LastName],[Biography],[ModifiedDateTime]) VALUES('Tom','Clancy','Tom was born in 1982.','2016-05-23 23:34:21');";
                var command = new SQLiteCommand(sql, conn);
                conn.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}