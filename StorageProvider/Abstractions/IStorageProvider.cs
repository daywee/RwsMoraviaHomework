namespace StorageProvider.Abstractions
{
    public interface IStorageProvider
    {
        IStorage GetStorage(string scheme);
    }
}
