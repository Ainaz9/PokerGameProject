namespace PokerGameRSF.Models
{
    /// <summary>
    /// Игровая фаза игры
    /// </summary>
    public enum GamePhase
    {
        PreFlop,
        Flop,
        Turn,
        River,
        Showdown,
        Completed
    }
}
