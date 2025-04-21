using Newtonsoft.Json;

namespace AL_Nibras_Ecom_API.Classes
{
    public class NestedJsonConverter<T> : JsonConverter<T>
    {
        public override T ReadJson(JsonReader reader, Type objectType, T existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
            {
                var jsonString = reader.Value?.ToString();
                if (!string.IsNullOrEmpty(jsonString))
                {
                    return JsonConvert.DeserializeObject<T>(jsonString);
                }
            }
            return default(T);
        }

        public override void WriteJson(JsonWriter writer, T value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
