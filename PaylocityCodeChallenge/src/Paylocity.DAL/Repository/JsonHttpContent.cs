using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Paylocity.DAL.Repository
{
    public class JsonHttpContent : HttpContent
    {
        private object Value { get; set; }

        public JsonHttpContent(Object value)
        {
            this.Value = value;
        }


        protected override async Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            using (var streamWriter = new StreamWriter(stream, new UTF8Encoding(false), 1024, true))
            {
                using (var jsonTextWriter = new JsonTextWriter(streamWriter) { Formatting = Formatting.None })
                {
                    var jsonSerializer = new JsonSerializer();
                    jsonSerializer.Serialize(jsonTextWriter, Value);
                    jsonTextWriter.Flush();
                }
            }

        }

        protected override bool TryComputeLength(out long length)
        {
            length = -1;
            return false;
        }
    }
}
