﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.API.Authentication
{
    public interface ICanGenerateTokens<T>
    {
        string Generate(T payload);
    }
}
