using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTest.Model;

namespace OnlineTest.Services.Interface
{
    public interface IRTokenService
    {
        bool AddToken(RToken token);
        bool ExpireToken(RToken token);
        RToken GetToken(string refreshToken);
    }
}
