using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.Model.Storage;
using System.Linq;
using H.Skeepy.Model;
using FluentAssertions;

namespace H.Skeepy.Testicles.Model.Storage
{
    [TestClass]
    public class PartyDtoOperations
    {
        [TestMethod]
        public void PartyDto_IsSerializable()
        {
            var dto = new PartyDto { Id = "Id", Name = "Fed" };
            dto.DetailsHolder.Details = new(string, string)[] { ("Detail1", "bla"), ("Detail2", "burp") }
                .Select(x => new DetailsHolderDto.Entry { Key = x.Item1, Value = x.Item2 }).ToArray();

            SerializableChecker.CheckSerializationInAllFormats(dto);
        }

        [TestMethod]
        public void PartyDto_CanTransitionToAndFro_Model()
        {
            var fed = Individual.New("Roger", "Federer"); fed.SetDetails(("Rank", "1"), ("Bla", "Burp"));
            var rafa = Individual.New("Rafael", "Nadal"); rafa.SetDetails(("Rank", "2"), ("Blabbing", "Burping"));
            var party = Party.New("GOAT", fed, rafa);
            party.SetDetails(("AwesomeLevel", "MAX"), ("IsClassic", "Absolutely"));
            var dto = party.ToDto();

            dto.Id.Should().Be(party.Id);
            dto.Name.Should().Be(party.Name);
            dto.DetailsHolder.Details.Select(x => (x.Key, x.Value)).Should().BeEquivalentTo(party.Details.Select(x => (x.Key, x.Value)));
            dto.Members.ShouldAllBeEquivalentTo(new IndividualDto[] { fed.ToDto(), rafa.ToDto() });

            dto.ToSkeepy().ShouldBeEquivalentTo(party);
        }

        [TestMethod]
        public void PartyDto_CanKeepReferenceOfInitialIndividuals()
        {
            var fed = Individual.New("Fed");
            var party = fed.ToParty();

            var dto = party.ToDto().WithMembersProvider(id => party[id]);

            dto.ToSkeepy().Members.Should().BeEquivalentTo(fed);
        }
    }
}
