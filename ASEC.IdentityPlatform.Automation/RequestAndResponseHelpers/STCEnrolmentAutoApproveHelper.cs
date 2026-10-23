using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LAWTrust_API_Automation.RequestAndResponseHelpers
{
   public class STCEnrolmentAutoApproveHelper
    {
        public RestRequest request;
        public static string accessToken;

        public RestRequest PostAutoAppovalEnrolment<T>(T payload) where T : class
        {
            request = new RestRequest()
            {
                Method = Method.Post
            };
            request.AddBody(payload);
            request.AddHeader("Authorization", $"Bearer {accessToken}");
            return request;
        }
    }
}
