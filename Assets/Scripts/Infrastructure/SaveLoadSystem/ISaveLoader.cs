public interface ISaveLoader
{
    void SaveGame(IGameRepository gameRepository, GameContext gameContext);
    void LoadGame(IGameRepository gameRepository, GameContext gameContext);
}