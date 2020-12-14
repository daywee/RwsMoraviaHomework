using Moravia.Homework.DocumentConverter.Abstractions;
using Moravia.Homework.DocumentConverter.Converters;
using Moravia.Homework.DocumentConverter.Models;
using Moravia.Homework.DocumentConverter.Serializers;
using StorageProvider.Abstractions;
using StorageProvider.Storage;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleApp
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            // Initialize dependencies.
            var jsonSerializer = new JsonDocumentSerializer();
            var xmlSerializer = new XmlDocumentSerializer();
            var converter = new ConverterBase<Document>(new ISerializer<Document>[] { jsonSerializer, xmlSerializer, });

            var webStorage = new WebStorage();
            var fileSystemStorage = new FileSystemStorage();
            var storageProvider = new StorageProvider.Storage.StorageProvider(new IStorage[] { webStorage, fileSystemStorage, });

            var app = new Application(converter, storageProvider);

            // Run application.
            var fromUri = new Uri("https://gist.githubusercontent.com/daywee/155b9145f00967cffac2933869614d6c/raw/835a0feef86d671077a210b92e11f318abd0acb5/rwsDocument.json");

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var toUri = new Uri(Path.Combine(documentsPath, "rwsHomework.xml"));

            await app.Convert(fromUri, toUri);
        }
    }
}
