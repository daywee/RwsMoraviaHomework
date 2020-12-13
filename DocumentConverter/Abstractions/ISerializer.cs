using System.IO;
using System.Threading.Tasks;

namespace Moravia.Homework.DocumentConverter.Abstractions
{
    public interface ISerializer<T>
    {
        public FileType FileType { get; }
        Task<T> DeserializeAsync(Stream stream);
        Task SerializeAsync(Stream stream, T @object);
    }
}
