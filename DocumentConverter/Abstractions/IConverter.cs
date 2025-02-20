﻿using System.IO;
using System.Threading.Tasks;

namespace Moravia.Homework.DocumentConverter.Abstractions
{
    public interface IConverter
    {
        public Task ConvertAsync(Stream source, FileType sourceFileType, Stream target, FileType targetFileType);
    }
}
