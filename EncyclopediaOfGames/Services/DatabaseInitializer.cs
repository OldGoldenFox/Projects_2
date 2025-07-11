using Microsoft.Data.Sqlite;

namespace EncyclopediaOfGames.Services
{
    internal static class DatabaseInitializer
    {
        public static void Initialize(string dbPath)
        {
            using var connection = new SqliteConnection($"Data Source={dbPath}");
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Games (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Title TEXT NOT NULL,
                    Genre TEXT NOT NULL
                );
            ";
            command.ExecuteNonQuery();
        }
    }
}
