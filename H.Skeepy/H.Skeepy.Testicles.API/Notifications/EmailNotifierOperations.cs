using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Net.Mail;
using H.Skeepy.API.Notifications;

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
        }
    }
}
