using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JsonApi
{
    /// <inheritdoc />
    public class Resolver : IResolver
    {
        private readonly List<IApiHandler> _handlers;
        private readonly List<Type> _knownModels = new();
        private readonly JsonSerializerOptions _serializerOptions;

        /// <summary>
        /// </summary>
        /// <param name="handlers"></param>
        public Resolver(IEnumerable<IApiHandler> handlers)
        {
            _handlers = handlers.ToList();
            _serializerOptions = new JsonSerializerOptions
            {
                Converters =
                {
                    new JsonStringEnumConverter()
                }
            };

            _handlers.ForEach(RegisterHandler);
        }

        /// <inheritdoc />
        public void CallHandler(string json)
        {
            var data = Deserialize(json);
            var handler = GetHandler(data.GetType());
            CheckPreconditions(data, handler);
            handler.Handle(data);
        }

        private object Deserialize(string item)
        {
            var wrapper = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(item, _serializerOptions);
            var type = _knownModels.First(x => x.Name == wrapper?.Keys.First());
            var serializedContent = wrapper?.Values.First().ToString();
            return JsonSerializer.Deserialize(serializedContent ?? string.Empty, type, _serializerOptions);
        }

        private void RegisterHandler(IApiHandler apiHandler)
        {
            _knownModels.Add(apiHandler.HandledType);
        }

        private IApiHandler GetHandler(Type jsonType)
        {
            return _handlers.First(x => x.HandledType == jsonType);
        }

        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        private static void CheckPreconditions(object data, IApiHandler handler)
        {
            if (handler.Disabled) throw new InvalidOperationException($"Handler of {data.GetType().Name} is disabled.");

            Validator.ValidateObject(data, new ValidationContext(data), true);
        }
    }
}