using StorageProvider.Abstractions;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace StorageProvider.Storage
{

    public class WebStorage : IStorage
    {
        public StorageType StorageType => StorageType.Http;

        public bool CanRead => true;

        public bool CanWrite => false;

        public async Task<Stream> OpenReadAsync(Uri uri)
        {
            using var client = new WebClient();
            return await client.OpenReadTaskAsync(uri);
        }

        public Task<Stream> OpenWriteAsync(Uri uri)
        {
            throw new NotSupportedException("WebStorage does not support write access.");
        }
    }
}
