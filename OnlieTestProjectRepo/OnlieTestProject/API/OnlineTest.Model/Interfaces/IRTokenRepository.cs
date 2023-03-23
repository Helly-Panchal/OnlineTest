namespace OnlineTest.Model.Interfaces
{
    public interface IRTokenRepository
    {
        RToken GetRefreshToken(int id, string refreshToken);
        bool AddRefreshToken(RToken token);
        bool ExpireRefreshToken(RToken token);
    }
}
