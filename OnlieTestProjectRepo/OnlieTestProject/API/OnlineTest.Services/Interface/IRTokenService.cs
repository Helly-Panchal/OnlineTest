using OnlineTest.Services.DTO.AddDTO;
using OnlineTest.Services.DTO.GetDTO;
using OnlineTest.Services.DTO.UpdateDTO;
using OnlineTest.Services.DTO;

namespace OnlineTest.Services.Interface
{
    public interface IRTokenService
    {
        GetRTokenDTO GetRefreshToken(RefreshDTO user);
        bool AddRefreshToken(AddRTokenDTO rToken);
        bool ExpireRefreshToken(UpdateRTokenDTO rToken);
    }
}
