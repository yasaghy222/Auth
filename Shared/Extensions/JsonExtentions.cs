using System.Text.Json;

namespace Auth.Shared.Extensions
{
    public static class JsonExtensions
    {
        public static string ToJson<T>(this T item) => JsonSerializer.Serialize(item);
        public static T? FromJson<T>(this string json) => JsonSerializer.Deserialize<T>(json);
    }
}