using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.API
{
    public class PasswordPolicy
    {
        public static void Validate(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new SkeepyApiException("The password cannot be empty");
            }
        }
    }
}
