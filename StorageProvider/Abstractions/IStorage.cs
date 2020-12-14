using System;
using System.IO;
using System.Threading.Tasks;

namespace StorageProvider.Abstractions
{
    public interface IStorage
    {
        bool CanRead { get; }
        bool CanWrite { get; }
        StorageType StorageType { get; }
        Task<Stream> OpenReadAsync(Uri uri);
        Task<Stream> OpenWriteAsync(Uri uri);
    }
}
