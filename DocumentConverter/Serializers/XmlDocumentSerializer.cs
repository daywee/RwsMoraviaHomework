using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

using Moravia.Homework.DocumentConverter.Abstractions;
using Moravia.Homework.DocumentConverter.Models;

namespace Moravia.Homework.DocumentConverter.Serializers
{
    public class XmlDocumentSerializer : ISerializer<Document>
    {
        private const string DocumentElement = "Document";
        private const string TitleElement = "Title";
        private const string TextElement = "Text";

        public async Task<Document> DeserializeAsync(Stream stream)
        {
            var tokenSource = new CancellationTokenSource();

            var xdocument = await XDocument.LoadAsync(stream, LoadOptions.None, tokenSource.Token);
            if (xdocument.Root.Name != DocumentElement)
                new ConversionException($"{DocumentElement} not found.");

            var title = xdocument.Root.Element(TitleElement)
                ?? throw new ConversionException($"{TitleElement} not found.");

            var text = xdocument.Root.Element(TextElement)
                ?? throw new ConversionException($"{TextElement} not found.");

            return new Document(title.Value, text.Value);
        }

        public async Task SerializeAsync(Stream stream, Document @object)
        {
            var tokenSource = new CancellationTokenSource();

            var xdocument = new XDocument(
                new XElement(DocumentElement,
                    new XElement(TitleElement, @object.Title),
                    new XElement(TextElement, @object.Text)));

            await xdocument.SaveAsync(stream, SaveOptions.None, tokenSource.Token);
        }
    }
}
