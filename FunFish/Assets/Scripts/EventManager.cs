


public static class EventManager
{

    public delegate void MonsterDestroyedHandler();
    public static event MonsterDestroyedHandler OnMonsterDestroyed;

    public delegate void GameOverHandler();
    public static event GameOverHandler OnGameOver;



    public static void MonsterDestroyed()
    {
        OnMonsterDestroyed?.Invoke();
    }

    public static void GameOver()
    {
        OnGameOver?.Invoke();
    }
}