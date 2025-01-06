using System.Data.SQLite;

namespace WpfTestApp
{
    public class DatabaseManager
    {
        private readonly SQLiteConnection _connection;

        public DatabaseManager(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public void CreateDatabase()
        {
            string dbPath = "PlayScore.db";

            if (!System.IO.File.Exists(dbPath))
            {
                SQLiteConnection.CreateFile(dbPath);
                Console.WriteLine("Database file created.");
            }
        }

        public void CreateTable(string tableName)
        {
            if (_connection.State != System.Data.ConnectionState.Open)
            {
                _connection.Open();
            }

            using (SQLiteCommand command = new SQLiteCommand($"CREATE TABLE IF NOT EXISTS [{tableName}] (Id INTEGER PRIMARY KEY, Name TEXT);", _connection))
            {
                try
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("Table Created or already exists.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
