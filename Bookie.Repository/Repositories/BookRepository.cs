using System.Collections.Generic;
using System.Data.SQLite;
using Bookie.Common.Entities;
using Bookie.Repository.Interfaces;

namespace Bookie.Repository.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ISQLiteRepository _repository;

        public BookRepository(ISQLiteRepository repository)
        {
            _repository = repository;
        }


        public List<Book> GetAllBooks()
        {
            using (var conn = _repository.Connection())
            {
                var sql = "SELECT * FROM Books";
                var command = new SQLiteCommand(sql, conn);
                conn.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var s = reader;
                    var title = (string) reader["Title"];
                }
            }
            return new List<Book>();
        }
    }
}