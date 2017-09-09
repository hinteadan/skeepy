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
        public enum AccountStatus
        {
            PendingValidation,
            PendingSetPassword,
            Valid,
        }

        public RegisteredUser()
        {

        }

        public RegisteredUser(ApplicantDto applicant)
        {
            FirstName = applicant.FirstName;
            LastName = applicant.LastName;
            Email = applicant.Email;
            Status = AccountStatus.PendingValidation;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string SkeepyId { get; set; }
        public string Id => Email;

        public AccountStatus Status { get; set; } = AccountStatus.PendingValidation;

        public string FullName()
        {
            return $"{FirstName} {LastName}".Trim();
        }
    }
}
