using Models.Models;

namespace Database.Interfaces
{
    public interface IRefreshTokenRepositoryService : IRepositoryService<RefreshToken>
    {
        void RemoveRefreshTokenForUser(string userId);

        RefreshToken Get(string id);
    }
}