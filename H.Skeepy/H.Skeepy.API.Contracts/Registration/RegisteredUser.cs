using H.Skeepy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.API.Contracts.Registration
{
    public class RegisteredUser : DetailsHolder, IHaveId
    {
        public static readonly string FacebookDetailsPrefix = "FB:";

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
            SetDetails(applicant?.FacebookDetails?.Details?.Select(x => ($"{FacebookDetailsPrefix}{x.Key}", x.Value)) ?? Enumerable.Empty<(string, string)>());
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

        public bool IsConfirmed()
        {
            return Status != AccountStatus.PendingValidation && Status != AccountStatus.PendingSetPassword;
        }
    }
}
