using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

using Moravia.Homework.DocumentConverter.Abstractions;
using Moravia.Homework.DocumentConverter.Models;

namespace Moravia.Homework.DocumentConverter.Serializers
{
    public class JsonDocumentSerializer : ISerializer<Document>
    {
        public async Task<Document> DeserializeAsync(Stream stream)
        {
            return await JsonSerializer.DeserializeAsync<Document>(stream);
        }

        public async Task SerializeAsync(Stream stream, Document @object)
        {
            await JsonSerializer.SerializeAsync(stream, @object);
        }
    }
}
