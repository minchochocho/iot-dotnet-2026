using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WpfBusanFestivalApp.model {
    internal class SingleOrArrayConverter<T> : JsonConverter<List<T>> {
        public override List<T> ReadJson(JsonReader reader, Type objectType, List<T>? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return [];

            JToken token = JToken.Load(reader);
            if (token.Type == JTokenType.Array)
                return token.ToObject<List<T>>(serializer) ?? [];

            if (token.Type == JTokenType.Object)
                return [token.ToObject<T>(serializer)!];

            return [];
        }

        public override void WriteJson(JsonWriter writer, List<T>? value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
