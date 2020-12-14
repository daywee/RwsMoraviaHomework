using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Moravia.Homework.DocumentConverter.Abstractions;
using Moravia.Homework.DocumentConverter.Models;
using Moravia.Homework.DocumentConverter.Serializers;

using Xunit;

namespace Moravia.Homework.DocumentConverter.Tests
{
    public class DocumentSerializerTests
    {
        [Theory]
        [ClassData(typeof(DocumentSerializers))]
        public async Task CanSerializeDeserialize(ISerializer<Document> serializer)
        {
            var stream = new MemoryStream();

            var expected = new Document
            {
                Title = "StringTitle",
                Text = "Some text.",
            };

            await serializer.SerializeAsync(stream, expected);
            stream.Position = 0;
            var result = await serializer.DeserializeAsync(stream);

            Assert.Equal(expected.Title, result.Title);
            Assert.Equal(expected.Text, result.Text);
        }
    }

    public class DocumentSerializers : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new XmlDocumentSerializer() };
            yield return new object[] { new JsonDocumentSerializer() };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
