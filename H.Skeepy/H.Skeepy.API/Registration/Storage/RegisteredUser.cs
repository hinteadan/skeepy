using H.Skeepy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.API.Registration.Storage
{
    public class RegisteredUser : IHaveId
    {
        public RegisteredUser()
        {

        }

        public RegisteredUser(ApplicantDto applicant)
        {
            FirstName = applicant.FirstName;
            LastName = applicant.LastName;
            Email = applicant.Email;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Id => Email;
    }
}
