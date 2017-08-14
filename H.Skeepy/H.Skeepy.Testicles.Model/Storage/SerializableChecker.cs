using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Testicles.Model.Storage
{
    public static class SerializableChecker
    {
        public static void CheckSerializationInAllFormats<T>(T dto)
        {
            CheckSerializationInBinary(dto);
            CheckSerializationIndtoContract(dto);
            CheckSerializationInXml(dto);
            CheckSerializationInJson(dto);
        }

        private static void CheckSerializationInBinary<T>(T dto)
        {
            dto.Should().BeBinarySerializable();
        }

        private static void CheckSerializationIndtoContract<T>(T dto)
        {
            dto.Should().BeDataContractSerializable();
        }

        private static void CheckSerializationInXml<T>(T dto)
        {
            dto.Should().BeXmlSerializable();
        }

        private static void CheckSerializationInJson<T>(T dto)
        {
            JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(dto)).ShouldBeEquivalentTo(dto);
        }
    }
}
