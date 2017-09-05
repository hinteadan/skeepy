using CryptoHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.API
{
    public sealed class Hasher
    {
        public static string Hash(string value)
        {
            return Crypto.HashPassword(value);
        }

        public static bool Verify(string hash, string value)
        {
            return Crypto.VerifyHashedPassword(hash, value);
        }
    }
}
