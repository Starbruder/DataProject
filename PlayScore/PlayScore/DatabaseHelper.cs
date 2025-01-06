using System.Data.SQLite;

namespace WpfTestApp
{
    public class DatabaseHelper
    {
        private readonly SQLiteConnection _connection;

        public DatabaseHelper(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public void ConnectToDatabase()
        {
            if (_connection.State != System.Data.ConnectionState.Open)
            {
                try
                {
                    _connection.Open();
                    Console.WriteLine("Database connected.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
