using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Net.Mail;
using H.Skeepy.API.Notifications;
using System.Linq;
using FluentAssertions;

namespace H.Skeepy.Testicles.API.Notifications
{
    [TestClass]
    public class EmailNotifierOperations
    {
        private string tmpEmailStorageFolder;
        private SmtpClient mailClient;

        [TestInitialize]
        public void Init()
        {
            tmpEmailStorageFolder = Path.Combine(Path.GetTempPath(), $"SkeepyEmailTests_{Guid.NewGuid()}");
            Directory.CreateDirectory(tmpEmailStorageFolder);
            mailClient = new SmtpClient();
            mailClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
            mailClient.PickupDirectoryLocation = tmpEmailStorageFolder;
        }

        [TestCleanup]
        public void Uninit()
        {
            mailClient.Dispose();
            Directory.Delete(tmpEmailStorageFolder, true);
        }

        [TestMethod]
        public void EmailNotifier_SendsEmail()
        {
            new EmailNotifier(mailClient).Notify(new NotificationDestination("a@a.com", "Test"), "Registration", "Content").Wait();
            var emails = Directory.EnumerateFiles(tmpEmailStorageFolder);
            emails.Should().HaveCount(1);
            var message = File.ReadAllLines(emails.Single());
            message.Single(x => x.StartsWith("Subject:")).Should().EndWith("Registration");
            message.Single(x => x.StartsWith("Content-Type:")).Should().Contain("text/html");
            message.Single(x => x.StartsWith("To:")).Should().Contain("a@a.com");
            message.Single(x => x.StartsWith("From:")).Should().Contain("no-reply-registration@skeepy.ro").And.Contain("SKeepy Registration");
        }
    }
}
