using H.Skeepy.API.Contracts.Authentication;
using H.Skeepy.API.Contracts.Notifications;
using H.Skeepy.API.Contracts.Registration;
using H.Skeepy.Core.Storage;
using H.Skeepy.Logging;
using H.Skeepy.Model;
using NLog;
using System;
using System.IO;
using System.Net.Mail;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace H.Skeepy.API.Registration
{
    public class RegistrationFlow
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

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
            using (log.Timing($"Load template {templateResourcePath}", LogLevel.Info))
            {
                var assembly = Assembly.GetExecutingAssembly();
                using (Stream stream = assembly.GetManifestResourceStream(templateResourcePath))
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
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
            using (log.Timing($"Apply for Skeepy account for {applicant.Email}", LogLevel.Info))
            {
                await ValidateApplicant(applicant);
                var token = tokenGenerator.Generate(applicant.Email);
                var user = new RegisteredUser(applicant);
                await userStore.Put(user);
                await tokenStore.Put(token);
                await notifier.Notify(new NotificationDestination(user.Email, user.FullName()), "SKeepy Registration", GenerateNotificationBody(user, token));
                return token;
            }
        }

        private string GenerateNotificationBody(RegisteredUser user, Token token)
        {
            using (log.Timing("Generate Registration Notification Body", LogLevel.Info))
            {
                return registrationEmailTemplate.Value.Compile(
                ("Name", user.FullName()),
                ("ValidationUrl", $"{baseUrl}/validate/{token.Public}")
                );
            }
        }

        private async Task ValidateApplicant(ApplicantDto applicant)
        {
            if (string.IsNullOrWhiteSpace(applicant.FirstName) && string.IsNullOrWhiteSpace(applicant.LastName))
            {
                throw new SkeepyApiException("At least one name, first or last, must be provided.");
            }
            ValidateEmailAddressFormat(applicant.Email);
            await ValidateEmailAddressAvailability(applicant.Email);
        }

        private async Task ValidateEmailAddressAvailability(string email)
        {
            if (await userStore.Get(email) != null)
            {
                throw new SkeepyApiException("Email address is already registered");
            }
        }

        private static void ValidateEmailAddressFormat(string email)
        {
            try
            {
                new MailAddress(email).Address.Equals(email, StringComparison.InvariantCultureIgnoreCase);
            }
            catch (Exception ex)
            {
                throw new SkeepyApiException("Invalid email address", ex);
            }
        }

        public async Task<Token> Validate(string publicToken)
        {
            using (log.Timing($"Validate Skeepy Registration token {publicToken}", LogLevel.Info))
            {
                var token = await tokenStore.Get(publicToken);
                if (token == null || token.HasExpired())
                {
                    log.Info($"Skeepy registration has expired for token {publicToken}");
                    return token;
                }

                var applicant = await userStore.Get(token.UserId) ?? throw new InvalidOperationException($"Inexistent applicant {token.UserId}");

                applicant.Status = RegisteredUser.AccountStatus.PendingSetPassword;

                await userStore.Put(applicant);

                return token;
            }
        }

        public async Task SetPassword(string publicToken, string password)
        {
            using (log.Timing($"Set password and activate Skeepy account for {publicToken}", LogLevel.Info))
            {
                PasswordPolicy.Validate(password);

                var token = await Validate(publicToken);
                if (token == null || token.HasExpired())
                {
                    throw new SkeepyApiException("The application has expired. You can apply for a new registration.");
                }

                var user = await userStore.Get(token.UserId) ?? throw new SkeepyApiException($"Inexistent applicant {token.UserId}");
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
}
