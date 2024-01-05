using System.Text.Json.Serialization;
using System.Text.Json;

namespace WebApp
{
    public class JsonIntConverter : JsonConverter<int>
    {
        public override int Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Number)
            {
                return reader.GetInt32();
            }

            // 他の処理が必要な場合はここに追加

            throw new JsonException($"Cannot convert {reader.TokenType} to {typeof(int)}.");
        }

        public override void Write(
            Utf8JsonWriter writer,
            int value,
            JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value);
        }
    }

    public class JsonNullableIntConverter : JsonConverter<int?>
    {
        public override int? Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            if (reader.TokenType == JsonTokenType.Number)
            {
                return reader.GetInt32();
            }

            // 他の処理が必要な場合はここに追加
            if ( reader.TokenType == JsonTokenType.String)
            {
                var str = reader.GetString();
                if (string.IsNullOrWhiteSpace(str))
                    return null;

                return int.Parse(str);
            }
            return null;
            //throw new JsonException($"Cannot convert {reader.TokenType} to {typeof(int?)}.");
        }

        public override void Write(
            Utf8JsonWriter writer,
            int? value,
            JsonSerializerOptions options)
        {
            if (value.HasValue)
            {
                writer.WriteNumberValue(value.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
        }
    }

}
