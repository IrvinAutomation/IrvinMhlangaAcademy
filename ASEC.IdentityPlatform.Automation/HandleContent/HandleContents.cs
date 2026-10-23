using Newtonsoft.Json;
using RestSharp;

namespace LAWTrust_API_Automation.HandleContent
{
    public static class HandleContents
    {
        //generic method returing different data types depending on the content of a response - And Deserialise the contents
        //public static T GetContent<T>(RestResponse response)
        //{
        //    var content = response.Content;
        //    return JsonConvert.DeserializeObject<T>(content);
        //}

        public static T? GetContent<T>(RestResponse response)
        {
            // Add this debugging
            Console.WriteLine($"Response Status: {response.StatusCode}");
            Console.WriteLine($"Response Content Type: {response.ContentType}");
            Console.WriteLine($"Response Content (first 500 chars): {response.Content?.Substring(0, Math.Min(500, response.Content?.Length ?? 0))}");

            if (response.Content == null)
                throw new InvalidOperationException("Response content is null");

            return JsonConvert.DeserializeObject<T>(response.Content);
        }
        //Serializing the content
        public static string SerializeJsonString(dynamic content)
        {
            return JsonConvert.SerializeObject(content, Formatting.Indented);   
        }
    }
}
