using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Auth.Shared.Extensions
{
    public class UlidToBytesConverter(ConverterMappingHints? mappingHints = null) : ValueConverter<Ulid, byte[]>(
            convertToProviderExpression: x => x.ToByteArray(),
            convertFromProviderExpression: x => new Ulid(x),
            mappingHints: DefaultHints.With(mappingHints ?? DefaultHints))
    {
        public UlidToBytesConverter() : this(null)
        {
        }

        private static readonly ConverterMappingHints DefaultHints = new(size: 16);
    }

    public class UlidToStringConverter(ConverterMappingHints? mappingHints = null) : ValueConverter<Ulid, string>(
            convertToProviderExpression: x => x.ToString(),
            convertFromProviderExpression: x => Ulid.Parse(x),
            mappingHints: DefaultHints.With(mappingHints ?? DefaultHints))
    {

        public UlidToStringConverter() : this(null)
        {
        }

        private static readonly ConverterMappingHints DefaultHints = new(size: 26);
    }

    public class UlidJsonConverter : JsonConverter<Ulid>
    {
        public override Ulid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var ulidString = reader.GetString();
            return Ulid.Parse(ulidString);
        }

        public override void Write(Utf8JsonWriter writer, Ulid value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
