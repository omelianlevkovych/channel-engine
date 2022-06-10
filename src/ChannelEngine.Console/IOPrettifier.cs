using System.Text.Json;

namespace ChannelEngine.Console
{
    internal static class IOPrettifier
    {
        public static string GetPrettyConsoleMesssage<T>(T result)
        {
            ArgumentNullException.ThrowIfNull(result);

            var responseText = JsonSerializer.Serialize<T>(result, new JsonSerializerOptions
            {
                WriteIndented = true,
            });

            return responseText;
        }
    }
}
