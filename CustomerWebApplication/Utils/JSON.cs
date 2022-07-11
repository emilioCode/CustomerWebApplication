using System.Text.Json;

namespace CustomerWebApplication.Utils
{
    public class JSON
    {
        public static string Stringify(object value)
        {
            return JsonSerializer.Serialize(value);
        }

        public static T Parse<T>(string json, JsonSerializerOptions options = null) where T: new()
        {
            return JsonSerializer.Deserialize<T>(json, options);
        }

    }
}
