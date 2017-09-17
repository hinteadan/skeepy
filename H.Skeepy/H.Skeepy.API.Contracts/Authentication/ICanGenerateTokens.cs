using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.API.Contracts.Authentication
{
    public interface ICanGenerateTokens<T>
    {
        Token Generate(T payload);
    }
}
