using System.Reflection;
using System.Text.Json;
using FastEndpoints;

namespace Auth.Shared.Extensions
{
    public static class PropertyExtensions
    {
        public static bool HasProp<TEntity>(this TEntity item, string key)
        {
            if (item is null) return false;
            Type t = item.GetType();
            PropertyInfo? prop = t.GetProperty(key) ?? null;
            return prop != null;
        }

        public static bool SetPropValue<TEntity>(this TEntity item, string key, object? value)
        {
            if (item is null) return false;
            Type t = item.GetType();
            PropertyInfo? prop = t.GetProperty(key) ?? null;
            if (prop != null && prop.CanWrite)
            {
                prop.SetValue(item, value);
                return true;
            }
            return false;
        }

        public static TResult? GetProperty<TResult, TEntity>(this TEntity item, string key)
        {
            if (item is null) throw new Exception($"{typeof(TEntity).Name} item Is Null!");
            Type t = item.GetType();
            PropertyInfo? prop = t.GetProperty(key) ?? throw new Exception($"Property {key} at {t.Name} Not Found!");
            object? val = prop.GetValue(item);
            if (val is null) return default;
            string json = JsonSerializer.Serialize(val);
            return JsonSerializer.Deserialize<TResult>(json);
        }

        public static string GetIdAsString<TEntity>(this TEntity item)
        {
            string id = item.GetProperty<string, TEntity>("Id") ?? throw new Exception($"Id at {item?.GetType().Name} Not Found");
            return id;
        }

        public static TId GetId<TId, TEntity>(this TEntity item)
        {
            TId id = item.GetProperty<TId, TEntity>("Id") ?? throw new Exception($"Id at {item?.GetType().Name} Not Found");
            return id;
        }

        public static Ulid GetIdAsUlid<TEntity>(this TEntity item)
        {
            Ulid id = item.GetId<Ulid, TEntity>();
            return id;
        }

        public static Ulid GetPropertyAsUlid<TEntity>(this TEntity item, string key)
        {
            Ulid property = item.GetProperty<Ulid, TEntity>(key);
            return property;
        }

        public static string GetPropertyAsString<TEntity>(this TEntity item, string key)
        {
            string property = item.GetProperty<string, TEntity>(key) ?? string.Empty;
            return property;
        }

        public static DateTime GetPropertyAsDateTime<TEntity>(this TEntity item, string key)
        {
            DateTime property = item.GetProperty<DateTime, TEntity>(key);
            return property;
        }
    }
}
