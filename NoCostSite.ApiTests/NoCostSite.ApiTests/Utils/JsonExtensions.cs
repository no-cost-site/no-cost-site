using System;
using System.Text.Json;

namespace NoCostSite.ApiTests.Utils
{
    public static class JsonExtensions
    {
        public static string ToJson<T>(this T obj)
        {
            return JsonSerializer.Serialize(obj);
        }

        public static T ToObject<T>(this string json)
        {
            return JsonSerializer.Deserialize<T>(json);
        }

        public static object ToObject(this string json, Type type)
        {
            return JsonSerializer.Deserialize(json, type);
        }
    }
}