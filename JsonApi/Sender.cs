using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JsonApi
{
    /// <inheritdoc />
    public class Sender : ISender
    {
        private JsonSerializerOptions _serializerOptions;

        /// <inheritdoc />
        public string GetDataString(object content)
        {
            _serializerOptions = new JsonSerializerOptions
            {
                Converters =
                {
                    new JsonStringEnumConverter()
                }
            };

            var data = new Dictionary<string, object> { { content.GetType().Name, content } };
            return JsonSerializer.Serialize(data, _serializerOptions);
        }
    }
}