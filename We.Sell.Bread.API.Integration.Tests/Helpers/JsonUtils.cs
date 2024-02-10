using Newtonsoft.Json;

namespace We.Sell.Bread.API.Integration.Tests.Helpers
{
    public static class JsonUtils
    {
        public static T DeserializeToDto<T>(string stringObject) where T : class
        {
            ArgumentNullException.ThrowIfNull(stringObject);

            return JsonConvert.DeserializeObject<T>(stringObject) ??
                throw new InvalidOperationException($"{nameof(stringObject)} could not be deserialized to a specified data object");
        }
    }
}
