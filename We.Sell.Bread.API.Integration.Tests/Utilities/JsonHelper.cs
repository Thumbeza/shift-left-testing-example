using Newtonsoft.Json;

namespace We.Sell.Bread.API.Integration.Tests.Utilities
{
    public static class JsonHelper
    {
        public static T Deserialize<T>(string jsonString) where T : class 
        {
            if (string.IsNullOrEmpty(jsonString))
            {
                throw new ArgumentNullException("The json object cannot be null");
            } 

            var result = JsonConvert.DeserializeObject<T>(jsonString);

            return result ?? 
                throw new InvalidOperationException("The json object could be deserialized to the desired type.");
        }

        public static string Serialize(object objectToString)
        {
            return JsonConvert.SerializeObject(objectToString);
        }
    }
}
