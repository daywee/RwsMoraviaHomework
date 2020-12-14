using Moq;
using StorageProvider.Abstractions;
using Xunit;

namespace StorageProvider.Tests
{
    public class StorageProviderTests
    {
        [Theory]
        [InlineData("http", StorageType.Http)]
        [InlineData("https", StorageType.Http)]
        [InlineData("file", StorageType.FileSystem)]
        public void StorageProviderReturnsCorrectStorageForGivenType(string scheme, StorageType expectedStorageType)
        {
            var webStorageMock = MockStorageWithType(StorageType.Http);
            var fileStorageMock = MockStorageWithType(StorageType.FileSystem);

            var provider = new Storage.StorageProvider(new IStorage[] { webStorageMock, fileStorageMock });

            var actualStorageType = provider.GetStorage(scheme).StorageType;

            Assert.Equal(expectedStorageType, actualStorageType);
        }

        private IStorage MockStorageWithType(StorageType type)
        {
            var mock = new Mock<IStorage>();
            mock.Setup(e => e.StorageType).Returns(type);
            return mock.Object;
        }
    }
}
