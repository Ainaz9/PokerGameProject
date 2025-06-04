using PokerGamesRSF.DTO;

namespace PokerGamesRSF.Services
{
    public interface IUserService
    {
        Task<UserStatsDto> GetUserStatsAsync(Guid userId);
    }
}
