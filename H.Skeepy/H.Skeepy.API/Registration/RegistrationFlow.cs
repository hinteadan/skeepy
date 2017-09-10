using H.Skeepy.API.Authentication;
using H.Skeepy.API.Notifications;
using H.Skeepy.API.Registration.Storage;
using H.Skeepy.Core.Storage;
using H.Skeepy.Model;
using Nancy.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace H.Skeepy.API.Registration
{
    public class RegistrationFlow
    {
        private readonly ICanGenerateTokens<string> tokenGenerator;
        private readonly ICanManageSkeepyStorageFor<Token> tokenStore;
        private readonly ICanManageSkeepyStorageFor<RegisteredUser> userStore;
        private readonly ICanStoreSkeepy<Credentials> credentialStore;
        private readonly ICanStoreSkeepy<Individual> skeepyIndividualStore;
        private readonly ICanNotify notifier;
        private static readonly string templateResourcePath = "H.Skeepy.API.Notifications.HtmlRegistrationTemplate.html";
        private readonly Lazy<TemplateParser> registrationEmailTemplate = new Lazy<TemplateParser>(() => new TemplateParser(LoadTemplate()), LazyThreadSafetyMode.PublicationOnly);
#if DEBUG
        private readonly string baseUrl = "http://localhost:9901";
#else
        private readonly string baseUrl = "http://register.skeepy.ro";
#endif

        private static string LoadTemplate()
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(templateResourcePath))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public RegistrationFlow(ICanManageSkeepyStorageFor<RegisteredUser> userStore, ICanStoreSkeepy<Credentials> credentialStore, ICanStoreSkeepy<Individual> skeepyIndividualStore, ICanManageSkeepyStorageFor<Token> tokenStore, ICanGenerateTokens<string> tokenGenerator, ICanNotify notifier)
        {
            this.userStore = userStore ?? throw new InvalidOperationException($"Must provide a {nameof(userStore)}");
            this.credentialStore = credentialStore ?? throw new InvalidOperationException($"Must provide a {nameof(credentialStore)}");
            this.skeepyIndividualStore = skeepyIndividualStore ?? throw new InvalidOperationException($"Must provide a {nameof(skeepyIndividualStore)}");
            this.tokenStore = tokenStore ?? throw new InvalidOperationException($"Must provide a {nameof(tokenStore)}");
            this.tokenGenerator = tokenGenerator ?? throw new InvalidOperationException($"Must provide a {nameof(tokenGenerator)}");
            this.notifier = notifier ?? throw new InvalidOperationException($"Must provide a {nameof(notifier)}");
        }

        public async Task<Token> Apply(ApplicantDto applicant)
        {
            ValidateApplicant(applicant);
            var token = tokenGenerator.Generate(applicant.Email);
            var user = new RegisteredUser(applicant);
            await userStore.Put(user);
            await tokenStore.Put(token);
            await notifier.Notify(new NotificationDestination(user.Email, user.FullName()), "SKeepy Registration", GenerateNotificationBody(user, token));
            return token;
        }

        private string GenerateNotificationBody(RegisteredUser user, Token token)
        {
            return registrationEmailTemplate.Value.Compile(
                ("Name", user.FullName()),
                ("ValidationUrl", $"{baseUrl}/registration/validate/{HttpUtility.UrlEncode(user.Email)}/{token.Public}")
                );
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

        public async Task<Token> Validate(string publicToken)
        {
            var token = await tokenStore.Get(publicToken);
            if (token == null || token.HasExpired())
            {
                return token;
            }

            var applicant = await userStore.Get(token.UserId) ?? throw new InvalidOperationException($"Inexistent applicant {token.UserId}");

            applicant.Status = RegisteredUser.AccountStatus.PendingSetPassword;

            await userStore.Put(applicant);

            return token;
        }

        public async Task SetPassword(string publicToken, string password)
        {
            if(string.IsNullOrWhiteSpace(password))
            {
                throw new InvalidOperationException("Password cannot be empty");
            }

            var token = await Validate(publicToken);
            if (token == null || token.HasExpired())
            {
                throw new InvalidOperationException("The application has expired. You can apply for a new registration.");
            }

            var user = await userStore.Get(token.UserId) ?? throw new InvalidOperationException($"Inexistent applicant {token.UserId}");
            user.Status = RegisteredUser.AccountStatus.Valid;
            await credentialStore.Put(new Credentials(user.Id, Hasher.Hash(password)));
            var fed = Individual.New(user.FirstName, user.LastName);
            fed.SetDetail("Email", user.Email);
            await skeepyIndividualStore.Put(fed);
            user.SkeepyId = fed.Id;
            await userStore.Put(user);
            await tokenStore.Zap(publicToken);
        }
    }
}
