using StorageProvider.Abstractions;
using System;
using System.IO;
using System.Threading.Tasks;

namespace StorageProvider.Storage
{
    public class FileSystemStorage : IStorage
    {
        public StorageType StorageType => StorageType.FileSystem;

        public bool CanRead => true;

        public bool CanWrite => true;

        public Task<Stream> OpenReadAsync(Uri uri)
        {
            var stream = File.OpenRead(uri.AbsolutePath);
            return Task.FromResult(stream as Stream);
        }

        public Task<Stream> OpenWriteAsync(Uri uri)
        {
            var stream = File.OpenWrite(uri.AbsolutePath);
            return Task.FromResult(stream as Stream);
        }
    }
}
