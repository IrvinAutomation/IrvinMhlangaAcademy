using LAWTrust_API_Automation.DataTransferModels;
using LAWTrust_API_Automation.HandleContent;
using LAWTrust_API_Automation.RequestAndResponseHelpers;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAWTrust_API_Automation.MethodOperators
{
    public class ADSSChangePasswordOperator
    {
        public Helpers _helpers;
        public ADSSChangePasswordHelper _adssChangePasswordHelper;

        public ADSSChangePasswordOperator()
        {
            _helpers = new Helpers();
            _adssChangePasswordHelper = new ADSSChangePasswordHelper();
        }

        public ADSSChangePasswordResponse PostADSSChangePassword(dynamic payload)
        {
            var client = _helpers.SetUrl("EnrolmentAppService/api/v1/Enrolment/ChangeAdssPassword");
            var request = _adssChangePasswordHelper.PostADSSChangePassword(payload);
            request.RequestFormat = DataFormat.Json;
            var response = _helpers.GetResponse(client, request);
            var ADSSChangePassword = HandleContents.GetContent<ADSSChangePasswordResponse>(response);
            return ADSSChangePassword;
        }
    }
}
