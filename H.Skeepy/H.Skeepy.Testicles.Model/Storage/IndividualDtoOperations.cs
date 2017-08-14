using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.Model.Storage;
using System.Linq;
using H.Skeepy.Model;
using FluentAssertions;

namespace H.Skeepy.Testicles.Model.Storage
{
    [TestClass]
    public class IndividualDtoOperations
    {
        [TestMethod]
        public void IndividualDto_IsSerializable()
        {
            var dto = new IndividualDto { Id = "Id", FirstName = "Hintea", LastName = "Dan" };

            dto.DetailsHolder.Details = new(string, string)[] { ("Detail1", "bla"), ("Detail2", "burp") }
                .Select(x => new DetailsHolderDto.Entry { Key = x.Item1, Value = x.Item2 }).ToArray();

            SerializableChecker.CheckSerializationInAllFormats(dto);
        }

        [TestMethod]
        public void IndividualDto_CanTransitionToAndFro_Model()
        {
            var fed = Individual.New("Roger", "Federer");
            fed.SetDetails(("Ranking", "1"), ("Country", "Switzerland"));
            var dto = fed.ToDto();

            dto.Id.Should().Be(fed.Id);
            dto.FirstName.Should().Be(fed.FirstName);
            dto.LastName.Should().Be(fed.LastName);
            dto.DetailsHolder.Details.Select(x => (x.Key, x.Value)).Should().BeEquivalentTo(fed.Details.Select(x => (x.Key, x.Value)));

        }
    }
}
