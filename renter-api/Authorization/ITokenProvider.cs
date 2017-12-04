using System.Threading.Tasks;

namespace Authorization
{
    public interface ITokenProvider
    {
        Task<AuthToken> GenerateToken(string userName, string password);
        Task<AuthToken> RefreshToken(string refreshTokenId);
    }
}