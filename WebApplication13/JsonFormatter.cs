using System;
using System.IO;
using System.Net.Http;
using System.Net;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Text.Json;

namespace WebApplication13
{
    public class JsonFormatter : JsonMediaTypeFormatter
    {
        public JsonFormatter() {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
            SupportedEncodings.Add(new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true));
            SupportedEncodings.Add(new UnicodeEncoding(bigEndian: false, byteOrderMark: true, throwOnInvalidBytes: true));
        }

        public override bool CanReadType(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            return true;
        }

        public override bool CanWriteType(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            return true;
        }

        public override async Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger, CancellationToken cancellationToken = default)
        {
            return await DeserializeFromStream(readStream, type, cancellationToken).ConfigureAwait(false);
        }

        public override async Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {
            return await DeserializeFromStream(readStream, type).ConfigureAwait(false);
        }

        public override async Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
        {
            await SerializeToStream(writeStream, value, type).ConfigureAwait(false);
        }

        private async Task<object> DeserializeFromStream(Stream readStream, Type type, CancellationToken cancellationToken = default)
        {
            try
            {
                return await JsonSerializer.DeserializeAsync(readStream, type, cancellationToken: cancellationToken).ConfigureAwait(false);

            }
            catch
            {
                return null;
            }
        }

        private async Task SerializeToStream(Stream readStream, object value, Type type, CancellationToken cancellationToken = default)
        {
            await JsonSerializer.SerializeAsync(readStream, value, type, cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}
