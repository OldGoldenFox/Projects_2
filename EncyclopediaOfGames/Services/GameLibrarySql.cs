using EncyclopediaOfGames.Interfaces;
using Microsoft.Data.Sqlite;

namespace EncyclopediaOfGames.Services
{
    internal class GameLibrarySql : IGameLibrary
    {
        private readonly string dbPath;

        public GameLibrarySql (string dbPath)
        {
            this.dbPath = dbPath;
        }

        public bool IsEmpty()
        {
            using var connection = new SqliteConnection($"Data Source={dbPath}");
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT COUNT(*) FROM Games;";
            long count = (long)command.ExecuteScalar();

            return count == 0;
        }

        public void AddGame(string title, string genre)
        {
            using var connection = new SqliteConnection($"Data Source={dbPath}");
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
            INSERT INTO Games (Title, Genre)
            VALUES ($title, $genre);
            ";

            command.Parameters.AddWithValue("$title", title.ToLower());
            command.Parameters.AddWithValue("$genre", genre.ToLower());

            command.ExecuteNonQuery();
        }

        public void RemoveGame(int gameIndex)
        {
            using var connection = new SqliteConnection($"Data Source = {dbPath}");
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Games WHERE Id = $id";
            command.Parameters.AddWithValue("$id", gameIndex);

            command.ExecuteNonQuery();
        }

        public void ShowAllGames()
        {
            using var connection = new SqliteConnection($"Data Source={dbPath}");
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT Id, Title, Genre FROM Games";

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string title = reader.GetString(1);
                string genre = reader.GetString(2);

                Console.WriteLine($"[Id: {id}]\t{title}\t({genre})");
            }
        }
    }
}
