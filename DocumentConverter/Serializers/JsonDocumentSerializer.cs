using Moravia.Homework.DocumentConverter.Abstractions;
using Moravia.Homework.DocumentConverter.Models;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Moravia.Homework.DocumentConverter.Serializers
{
    public class JsonDocumentSerializer : ISerializer<Document>
    {
        public FileType FileType => FileType.Json;

        private readonly JsonSerializerOptions _serializerOptions;

        public JsonDocumentSerializer() // Here could be injected options for serialization/deserilization i.e. preserve whitespace or case conventions.
        {
            _serializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        }

        public async Task<Document> DeserializeAsync(Stream stream)
        {
            return await JsonSerializer.DeserializeAsync<Document>(stream, _serializerOptions);
        }

        public async Task SerializeAsync(Stream stream, Document @object)
        {
            await JsonSerializer.SerializeAsync(stream, @object, _serializerOptions);
        }
    }
}
