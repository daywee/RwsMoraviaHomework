using Moravia.Homework.DocumentConverter;
using Moravia.Homework.DocumentConverter.Abstractions;
using StorageProvider.Abstractions;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Application
    {
        private readonly IConverter _converter;
        private readonly IStorageProvider _storageProvider;

        public Application(IConverter converter, IStorageProvider storageProvider)
        {
            _converter = converter;
            _storageProvider = storageProvider;
        }

        public async Task Convert(Uri fromUri, Uri toUri)
        {
            var fromFileType = GetFileExtension(fromUri);
            var toFileType = GetFileExtension(toUri);

            var fromStorage = _storageProvider.GetStorage(fromUri.Scheme);
            var toStorage = _storageProvider.GetStorage(toUri.Scheme);

            if (!fromStorage.CanRead)
                throw new Exception($"Cannot read from {fromStorage.StorageType} ({fromUri.AbsolutePath}).");
            if (!toStorage.CanWrite)
                throw new Exception($"Cannot write to {toStorage.StorageType} ({toUri.AbsolutePath}).");

            using (var fromStream = await fromStorage.OpenReadAsync(fromUri))
            using (var toStream = await toStorage.OpenWriteAsync(toUri))
            {
                await _converter.ConvertAsync(fromStream, fromFileType, toStream, toFileType);
            }
        }

        private FileType GetFileExtension(Uri uri)
        {
            string stringExtension = Path.GetExtension(uri.AbsolutePath)
                ?? throw new ArgumentException("Provided URI does not contain extension", nameof(uri));

            stringExtension = stringExtension[1..]; // Remove period.

            if (!Enum.TryParse<FileType>(stringExtension, true, out var result))
                throw new Exception($"File extension '{stringExtension}' not supported.");

            return result;
        }
    }
}
