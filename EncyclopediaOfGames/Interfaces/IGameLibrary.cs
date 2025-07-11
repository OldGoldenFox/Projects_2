namespace EncyclopediaOfGames.Interfaces
{
    internal interface IGameLibrary
    {
        bool IsEmpty();
        public void AddGame(string title, string genre);
        public void RemoveGame(int gameIndex);
        public void ShowAllGames();
    }
}
