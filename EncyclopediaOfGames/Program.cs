using EncyclopediaOfGames.Helpers;
using EncyclopediaOfGames.Interfaces;
using EncyclopediaOfGames.Services;

namespace EncyclopediaOfGames
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = InputHelper.ReadString("Введите название для таблицы: ") + ".db";

            DatabaseInitializer.Initialize(path);

            IGameLibrary library = new GameLibrarySql(path);

            if (!File.Exists(path))
            {
                Console.WriteLine($"Создана новая таблица {path}");
            }

            GameLibraryApp app = new GameLibraryApp(library);
            app.Run();
        }
    }
}
