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
}
