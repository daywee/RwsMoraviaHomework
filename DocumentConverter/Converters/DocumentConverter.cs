
using System;
using System.IO;
using System.Threading.Tasks;

using Moravia.Homework.DocumentConverter.Abstractions;
using Moravia.Homework.DocumentConverter.Models;
using Moravia.Homework.DocumentConverter.Serializers;

namespace Moravia.Homework.DocumentConverter.Converters
{
    public class DocumentConverter : IDocumentConverter
    {
        private readonly JsonDocumentSerializer _jsonSerializer;
        private readonly XmlDocumentSerializer _xmlSerializer;

        public DocumentConverter(JsonDocumentSerializer jsonSerializer, XmlDocumentSerializer xmlSerializer)
        {
            _jsonSerializer = jsonSerializer;
            _xmlSerializer = xmlSerializer;
        }

        public async Task ConvertAsync(Stream source, FileType sourceFileType, Stream target, FileType targetFileType)
        {
            var deserializer = GetSerializer(sourceFileType);
            var document = await deserializer.DeserializeAsync(source);

            var serializer = GetSerializer(targetFileType);
            await serializer.SerializeAsync(target, document);
        }

        private ISerializer<Document> GetSerializer(FileType fileType)
        {
            return fileType switch
            {
                FileType.Json => _jsonSerializer,
                FileType.Xml => _xmlSerializer,
                _ => throw new ArgumentOutOfRangeException(nameof(fileType), ""),
            };
        }
    }
}
