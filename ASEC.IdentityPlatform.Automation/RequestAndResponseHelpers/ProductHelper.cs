using RestSharp;
using System;

namespace LAWTrust_API_Automation.RequestAndResponseHelpers
{
    public class ProductHelper
    {
        public RestRequest request;
        public static string accessToken;
        public static String clientId;

        public RestRequest GetProduct()
        {
            request = new RestRequest()
            {
                Method = Method.Get
            };
            request.AddParameter("clientId", clientId);
            request.AddHeader("Authorization", $"Bearer {accessToken}");
            return request; 
        }
    }
}
