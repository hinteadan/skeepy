using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.Model.Storage;
using System.Linq;

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
    }
}
