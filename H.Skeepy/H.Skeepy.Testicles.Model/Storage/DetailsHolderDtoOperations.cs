using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.Model.Storage;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using FluentAssertions;
using Newtonsoft.Json;
using H.Skeepy.Model;
using System.Linq;

namespace H.Skeepy.Testicles.Model.Storage
{
    [TestClass]
    public class DetailsHolderDtoOperations
    {
        private class DetailsHolderSample : DetailsHolder
        {

        }

        [TestMethod]
        public void DetailsHolderDto_IsSerializable()
        {
            SerializableChecker.CheckSerializationInAllFormats(new DetailsHolderDto());
        }

        [TestMethod]
        public void DetailsHolderDto_CanTransition_ToAndFro_DetailsHolder()
        {
            var sample = new DetailsHolderSample().SetDetails(("Detail1", "bla, bla"), ("Detail2", "bla, burp"));
            var dto = DetailsHolderDto.From(sample);

            dto.Details.Select(x => (x.Key, x.Value)).Should().BeEquivalentTo(sample.Details.Select(x => (x.Key, x.Value)));

            sample.ShouldBeEquivalentTo(dto.ToSkeepy());
        }
    }
}
