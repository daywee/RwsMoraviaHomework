﻿using System.IO;
using System.Threading.Tasks;

namespace Moravia.Homework.DocumentConverter.Abstractions
{
    public interface ISerializer<T>
    {
        Task<T> DeserializeAsync(Stream stream);
        Task SerializeAsync(Stream stream, T @object);
    }
}
