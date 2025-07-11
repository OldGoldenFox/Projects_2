using EncyclopediaOfGames.Helpers;
using EncyclopediaOfGames.Interfaces;
using EncyclopediaOfGames.Models;

namespace EncyclopediaOfGames
{
    internal class GameLibraryApp
    {
        private readonly IGameLibrary library;

        public GameLibraryApp(IGameLibrary library)
        {
            this.library = library;
        }

        public void Run()
        {
            while (true)
            {
                ShowMenu();
                MenuOperations choise = GetOperationChoice();

                Console.WriteLine();

                if (NeedsDataButEmpty(choise))
                {
                    Console.WriteLine("Таблица пуста");
                    continue;
                }

                switch (choise)
                {
                    case MenuOperations.AddGame:
                        AddGame();
                        break;

                    case MenuOperations.RemoveGame:
                        RemoveGame();
                        break;

                    case MenuOperations.ShowAllGames:
                        ShowAllGames();
                        break;

                    case MenuOperations.Exit:
                        return;
                }
            }
        }
        static void ShowMenu()
        {
            Console.WriteLine("\n1) Добавить");
            Console.WriteLine("2) Удалить");
            Console.WriteLine("3) Показать все");
            Console.WriteLine("4) Выйти");
        }

        private MenuOperations GetOperationChoice()
        {
            return (MenuOperations)InputHelper.Choose("Выберите действие: ", Enum.GetValues(typeof(MenuOperations)).Length);
        }

        private bool NeedsDataButEmpty(MenuOperations choice)
        {
            return ((int)choice >= 2 && (int)choice <= 3) && library.IsEmpty();
        }


        private void AddGame()
        {
            string title = InputHelper.ReadString("Введите название: ");
            string genre = InputHelper.ReadString("Введите жанр: ");
            library.AddGame(title, genre);
        }

        private void RemoveGame()
        {
            library.ShowAllGames();
            int gameIndex = InputHelper.ReadInt("Выберите игру для удаления: ");
            library.RemoveGame(gameIndex);
        }

        private void ShowAllGames()
        {
            library.ShowAllGames();
        }
    }
}