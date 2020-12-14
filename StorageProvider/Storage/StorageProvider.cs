using StorageProvider.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorageProvider.Storage
{
    public class StorageProvider : IStorageProvider
    {
        private readonly IReadOnlyDictionary<StorageType, IStorage> _storages;

        public StorageProvider(IEnumerable<IStorage> storages)
        {
            _storages = storages.ToDictionary(e => e.StorageType);
        }

        public IStorage GetStorage(string scheme)
        {
            return scheme switch
            {
                "http" => _storages[StorageType.Http],
                "https" => _storages[StorageType.Http],
                "file" => _storages[StorageType.FileSystem],
                _ => throw new ArgumentOutOfRangeException(nameof(scheme), $"Scheme {scheme} not supported."),
            };
        }
    }
}
