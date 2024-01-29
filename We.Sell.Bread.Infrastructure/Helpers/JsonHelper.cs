using Newtonsoft.Json;
using We.Sell.Bread.Core.Validations;

namespace We.Sell.Bread.Infrastructure.Helpers
{
    public static class JsonHelper
    {
        public static string ReadJsonFile(string path)
        {
            Validate.NullOrEmptyArgument(path);

            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"The file could not be found: {path}");
            }

            using var result = new StreamReader(path);

            var jsonString = result.ReadToEnd();

            return jsonString;
        }

        public static T Deserialize<T>(string jsonString)
        {
            Validate.NullOrEmptyArgument(jsonString);

            var result = JsonConvert.DeserializeObject<T>(jsonString);

            return result ??
                throw new InvalidOperationException("The json object could be deserialized to the desired type.");
        }

        public static string Serialize(object objectToString)
        {
            return JsonConvert.SerializeObject(objectToString);
        }

        public static async Task StreamWriteAsync(object obj, string fileName)
        {
            await Task.Run(() => StreamWrite(obj, fileName)); 
        }

        private static void StreamWrite(object data, string fileName)
        {
            using var streamWriter = File.CreateText(fileName);
            using var jsonWriter = new JsonTextWriter(streamWriter);

            jsonWriter.Formatting = Formatting.Indented;
            JsonSerializer.CreateDefault().Serialize(jsonWriter, data);
        }
    }
}
