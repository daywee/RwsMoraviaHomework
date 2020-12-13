using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Moravia.Homework.DocumentConverter.Abstractions;

namespace Moravia.Homework.DocumentConverter.Converters
{
    public class ConverterBase<T> : IConverter
    {
        private readonly IReadOnlyDictionary<FileType, ISerializer<T>> _serializers;

        public ConverterBase(IEnumerable<ISerializer<T>> serializers)
        {
            _serializers = serializers.ToDictionary(e => e.FileType);
        }

        public async Task ConvertAsync(Stream source, FileType sourceFileType, Stream target, FileType targetFileType)
        {
            var document = await _serializers[sourceFileType].DeserializeAsync(source);
            await _serializers[targetFileType].SerializeAsync(target, document);
        }
    }
}
