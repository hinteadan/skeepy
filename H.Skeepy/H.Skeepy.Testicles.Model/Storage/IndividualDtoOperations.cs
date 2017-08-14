using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.Model.Storage;
using System.Linq;

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
    }
}
