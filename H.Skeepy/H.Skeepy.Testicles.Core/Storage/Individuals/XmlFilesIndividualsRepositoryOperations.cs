using System;
using H.Skeepy.Core.Storage;
using H.Skeepy.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using H.Skeepy.Core.Storage.Individuals;

namespace H.Skeepy.Testicles.Core.Storage.Individuals
{
    [TestClass]
    public class XmlFilesIndividualsRepositoryOperations : IndividualsRepositoryOperations
    {
        private static string tmpStorageFolder;

        public XmlFilesIndividualsRepositoryOperations()
            : base(() => new XmlFilesIndividualsStorage(tmpStorageFolder), TimeSpan.FromSeconds(0.5))
        {
        }

        [TestInitialize]
        public override void Init()
        {
            tmpStorageFolder = Path.Combine(Path.GetTempPath(), $"SkeepyXmlStorageTests_{Guid.NewGuid()}");
            base.Init();
        }

        [TestCleanup]
        public override void Uninit()
        {
            base.Uninit();
            Directory.Delete(tmpStorageFolder, true);
        }
    }
}
