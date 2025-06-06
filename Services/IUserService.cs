using PokerGameRSF.DTO;


namespace PokerGameRSF.Services
{
    public interface IUserService
    {
        Task<UserStatsDto> GetUserStatsAsync(Guid userId);
    }
}
