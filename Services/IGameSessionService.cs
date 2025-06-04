using PokerGamesRSF.DTO;
using PokerGamesRSF.Models;

namespace PokerGamesRSF.Services
{
    public interface IGameSessionService
    {
        Task<GameSession> StartGameAsync(Guid player1Id, Guid player2Id);
        Task<GameStateDto> GetGameStateAsync(Guid sessionId, Guid playerId);
        Task SendActionAsync(Guid sessionId, ActionDto actionDto);
        Task AdvancePhaseAsync(GameSession session, MyDbContext context);
        Task DetermineWinnerAsync(GameSession session, MyDbContext context);
    }
}
