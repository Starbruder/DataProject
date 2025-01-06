using System.Data.SQLite;

namespace PlayScore;

public class DatabaseManager(SQLiteConnection connection)
{
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
        if (connection.State != System.Data.ConnectionState.Open)
        {
            connection.Open();
        }

        using SQLiteCommand command = new SQLiteCommand($"CREATE TABLE IF NOT EXISTS [{tableName}] (Id INTEGER PRIMARY KEY, Name TEXT);", connection);
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
