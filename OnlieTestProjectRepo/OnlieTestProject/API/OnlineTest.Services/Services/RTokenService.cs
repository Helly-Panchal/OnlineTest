using AutoMapper;
using OnlineTest.Model.Interfaces;
using OnlineTest.Model;
using OnlineTest.Services.DTO.AddDTO;
using OnlineTest.Services.DTO.GetDTO;
using OnlineTest.Services.DTO.UpdateDTO;
using OnlineTest.Services.DTO;
using OnlineTest.Services.Interface;

namespace OnlineTest.Services.Services
{
    public class RTokenService : IRTokenService
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IRTokenRepository _rTokenRepository;
        #endregion

        #region Constructor
        public RTokenService(IMapper mapper, IRTokenRepository rTokenRepository)
        {
            _mapper = mapper;
            _rTokenRepository = rTokenRepository;
        }
        #endregion

        #region Methods
        public GetRTokenDTO GetRefreshToken(RefreshDTO user)
        {
            var result = _rTokenRepository.GetRefreshToken(user.Id, user.RefreshToken);
            if (result == null)
            {
                return null;
            }
            else
            {
                return _mapper.Map<GetRTokenDTO>(result);
            }
        }

        public bool AddRefreshToken(AddRTokenDTO rToken)
        {
            return _rTokenRepository.AddRefreshToken(_mapper.Map<RToken>(rToken));
        }

        public bool ExpireRefreshToken(UpdateRTokenDTO rToken)
        {
            return _rTokenRepository.ExpireRefreshToken(_mapper.Map<RToken>(rToken));
        }
        #endregion
    }
}
