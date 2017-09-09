using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.API.Notifications
{
    public class EmailNotifier : ICanNotify
    {
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
            var message = GenerateMessage(destination, summary, content);
            return mailClient.SendMailAsync(message)
                .ContinueWith(t => { message.Dispose(); });
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
