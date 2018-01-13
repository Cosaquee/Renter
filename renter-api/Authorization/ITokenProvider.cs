namespace Authorization
{
    public interface ITokenProvider
    {
        AuthToken GenerateToken(string userName, string password);

        AuthToken RefreshToken(string refreshTokenId);
    }
}