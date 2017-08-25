using H.Skeepy.Model;
using H.Skeepy.Model.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace H.Skeepy.Core.Storage.Individuals
{
    public class XmlFilesIndividualsStorage : ICanManageSkeepyStorageFor<Individual>
    {
        private readonly DirectoryInfo rootDir;
        private static readonly XmlSerializer serializer = new XmlSerializer(typeof(IndividualDto));

        public XmlFilesIndividualsStorage(string rootFolderPath)
        {
            rootDir = new DirectoryInfo(rootFolderPath);
            rootDir.Create();
        }

        public Task<bool> Any()
        {
            return Task.Run(() =>
            {
                return rootDir.EnumerateFiles("*.xml").Any();
            });
        }

        public void Dispose()
        {

        }

        public Task<Individual> Get(string id)
        {
            return Task.Run(() =>
            {
                var individualFilePath = IndividualFilePath(id);

                if (!File.Exists(individualFilePath))
                {
                    return null;
                }

                using (var reader = XmlReader.Create(individualFilePath))
                {
                    return LoadIndividual(id);
                }
            });
        }

        public Task<IEnumerable<LazyEntity<Individual>>> Get()
        {
            return Task.Run(() => rootDir
                    .EnumerateFiles("*.xml")
                    .Select(f => new LazyEntity<Individual>(Individual.Existing(IndividualIdFromFile(f), "_lazy"), x => LoadIndividual(x.Id)))
            );
        }

        private static string IndividualIdFromFile(FileInfo f)
        {
            return f.Name.Substring(0, f.Name.IndexOf('.'));
        }

        public Task Put(Individual model)
        {
            return Task.Run(() =>
            {
                using (var writer = XmlWriter.Create(IndividualFilePath(model.Id), new XmlWriterSettings { Indent = true }))
                {
                    serializer.Serialize(writer, model.ToDto());
                }
            });
        }

        private Individual LoadIndividual(string id)
        {
            using (var reader = XmlReader.Create(Path.Combine(rootDir.FullName, $"{id}.xml")))
            {
                return ((IndividualDto)serializer.Deserialize(reader)).ToSkeepy();
            }
        }

        public Task Zap(string id)
        {
            return Task.Run(() =>
            {
                File.Delete(IndividualFilePath(id));
            });
        }

        private string IndividualFilePath(string id)
        {
            return Path.Combine(rootDir.FullName, $"{id}.xml");
        }
    }
}
