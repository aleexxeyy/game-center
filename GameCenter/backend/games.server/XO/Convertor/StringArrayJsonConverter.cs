using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XO.Convertors
{
    public class StringArrayJsonConverter : JsonConverter<string[,]>
    {
        public override string[,] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartArray) throw new JsonException();

            var rows = new System.Collections.Generic.List<string[]>();
            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
            {
                if (reader.TokenType != JsonTokenType.StartArray) throw new JsonException();

                var row = new System.Collections.Generic.List<string>();
                while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    row.Add(reader.GetString() ?? throw new JsonException());

                rows.Add(row.ToArray());
            }

            var result = new string[rows.Count, rows[0].Length];
            for (int i = 0; i < rows.Count; i++)
                for (int j = 0; j < rows[i].Length; j++)
                    result[i, j] = rows[i][j];

            return result;
        }

        public override void Write(Utf8JsonWriter writer, string[,] value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            for (int i = 0; i < value.GetLength(0); i++)
            {
                writer.WriteStartArray();
                for (int j = 0; j < value.GetLength(1); j++)
                    writer.WriteStringValue(value[i, j]);
                writer.WriteEndArray();
            }
            writer.WriteEndArray();
        }
    }
}
