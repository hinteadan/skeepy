using H.Skeepy.API.Authentication;
using H.Skeepy.Core.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.API.Registration
{
    public class RegistrationFlow
    {
        private readonly ICanGenerateTokens<string> tokenGenerator;
        private readonly ICanManageSkeepyStorageFor<Token> tokenStore;

        public RegistrationFlow(ICanManageSkeepyStorageFor<Token> tokenStore, ICanGenerateTokens<string> tokenGenerator)
        {
            this.tokenStore = tokenStore ?? throw new InvalidOperationException($"Must provide a {nameof(tokenStore)}");
            this.tokenGenerator = tokenGenerator ?? throw new InvalidOperationException($"Must provide a {nameof(tokenGenerator)}");
        }

        public Task<Token> Apply(ApplicantDto applicant)
        {
            ValidateApplicant(applicant);
            var token = tokenGenerator.Generate(applicant.Email);
            return tokenStore.Put(token).ContinueWith(t => token);
        }

        private static void ValidateApplicant(ApplicantDto applicant)
        {
            if (string.IsNullOrWhiteSpace(applicant.FirstName) && string.IsNullOrWhiteSpace(applicant.LastName))
            {
                throw new InvalidOperationException("At least one name, first or last, must be provided.");
            }
            ValidateEmail(applicant.Email);
        }

        private static void ValidateEmail(string email)
        {
            try
            {
                new MailAddress(email).Address.Equals(email, StringComparison.InvariantCultureIgnoreCase);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Invalid email address", ex);
            }
        }

        public Task<Token> Validate(string publicToken)
        {
            return tokenStore.Get(publicToken);
        }
    }
}
