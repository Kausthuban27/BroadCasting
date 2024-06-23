using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace BroadCastingAPI.Services.Utilities
{
    public class DeserializeService<T>
    {
        public static T? DeserializeStream(Stream stream)
        {
            if (stream.Length == 0)
                return default;

            var options = new JsonSerializerOptions();
            options.Converters.Add(new JsonStringEnumConverter());
            options.PropertyNameCaseInsensitive = true;

            using var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            return JsonSerializer.Deserialize<T>(memoryStream.ToArray(), options);
        }
    }
}
