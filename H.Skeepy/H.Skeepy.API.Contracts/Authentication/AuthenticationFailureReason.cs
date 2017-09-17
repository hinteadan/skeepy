using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.API.Contracts.Authentication
{
    public enum AuthenticationFailureReason
    {
        None,
        InvalidCredentials,
        InvalidToken,
        TokenExpired,
        TokenExpiredAndTimedOut,
        Unknwon,
    }
}
