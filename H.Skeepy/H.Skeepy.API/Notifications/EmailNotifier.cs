using H.Skeepy.API.Contracts.Notifications;
using NLog;
using System;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.API.Notifications
{
    public class EmailNotifier : ICanNotify
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        private readonly MailAddress from = new MailAddress("no-reply-registration@skeepy.ro", "SKeepy Registration");
        private readonly SmtpClient mailClient;

        public EmailNotifier(SmtpClient mailClient)
        {
            this.mailClient = mailClient ?? throw new InvalidOperationException($"{nameof(mailClient)} must be provided");
        }

        public EmailNotifier()
            : this(new SmtpClient())
        { }

        public Task Notify(NotificationDestination destination, string summary, string content)
        {
            log.Info($"Sending email notification to {destination}, regarding \"{summary}\"...");
            var message = GenerateMessage(destination, summary, content);
            return mailClient
                .SendMailAsync(message)
                .ContinueWith(t =>
                {
                    message.Dispose();
                    log.Info($"Email notification successfully sent to {destination}, regarding \"{summary}\"");
                });
        }

        private MailMessage GenerateMessage(NotificationDestination to, string subject, string body)
        {
            var message = new MailMessage(from, new MailAddress(to.Address, to.Name));
            message.Body = body;
            message.IsBodyHtml = true;
            message.BodyEncoding = Encoding.UTF8;
            message.Subject = subject;
            return message;
        }

        public void Dispose()
        {
            if (mailClient != null)
            {
                mailClient.Dispose();
            }
        }
    }
}
