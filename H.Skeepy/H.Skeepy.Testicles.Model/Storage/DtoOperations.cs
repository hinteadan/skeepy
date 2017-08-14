using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.Model.Storage;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using FluentAssertions;
using Newtonsoft.Json;

namespace H.Skeepy.Testicles.Model.Storage
{
    [TestClass]
    public class DtoOperations
    {
        [TestMethod]
        public void DetailsHolderDto_CanBeSerialized_And_Deserialzied_InBinaryFormat()
        {
            var dto = new DetailsHolderDto();

            dto.Should().BeBinarySerializable();
        }

        [TestMethod]
        public void DetailsHolderDto_CanBeSerialized_And_Deserialzied_InDataContractFormat()
        {
            var dto = new DetailsHolderDto();

            dto.Should().BeDataContractSerializable();
        }

        [TestMethod]
        public void DetailsHolderDto_CanBeSerialized_And_Deserialzied_InXmlFormat()
        {
            var dto = new DetailsHolderDto();

            dto.Should().BeXmlSerializable();
        }

        [TestMethod]
        public void DetailsHolderDto_CanBeSerialized_And_Deserialzied_InJsonFormat()
        {
            var dto = new DetailsHolderDto();
            var reload = JsonConvert.DeserializeObject<DetailsHolderDto>(JsonConvert.SerializeObject(dto));
            reload.ShouldBeEquivalentTo(dto);
        }
    }
}
