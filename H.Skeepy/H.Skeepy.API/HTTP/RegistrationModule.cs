using H.Skeepy.API.Registration;
using Nancy;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.API.HTTP
{
    public class RegistrationModule : NancyModule
    {
        public RegistrationModule()
            : base("/registration")
        {
            Post["/apply"] = _ =>
            {
                var applicant = this.Bind<ApplicantDto>();

                if (!IsApplicantValid(applicant))
                {
                    return HttpStatusCode.UnprocessableEntity;
                }

                return HttpStatusCode.Accepted;
            };
        }

        private bool IsApplicantValid(ApplicantDto applicant)
        {
            return (!string.IsNullOrWhiteSpace(applicant.FirstName) || !string.IsNullOrWhiteSpace(applicant.LastName)) &&
                IsValidEmail(applicant.Email);
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                return new MailAddress(email).Address.Equals(email, StringComparison.InvariantCultureIgnoreCase);
            }
            catch
            {
                return false;
            }
        }
    }
}
