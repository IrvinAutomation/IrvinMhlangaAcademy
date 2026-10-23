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
    public class PostAutoAppovalEnrolmentOperator
    {
        public STCEnrolmentAutoApproveHelper _autoApprovalHelper;
        public Helpers _helpers;

        public PostAutoAppovalEnrolmentOperator()
        {
            _autoApprovalHelper = new STCEnrolmentAutoApproveHelper();
            _helpers = new Helpers();
        }
        public AutoApprovalEnrolmentResponse PostAutoApprovalEnrolmentUserDetails(dynamic payload)
        {
            var client = _helpers.SetUrl("EnrolmentAppService/api/v1/Enrolment/AutoApproveEnrolment");
            var request = _autoApprovalHelper.PostAutoAppovalEnrolment(payload);
            request.RequestFormat = DataFormat.Json;
            var response = _helpers.GetResponse(client, request);
            var updateAutoApprovedEnrolledUser = HandleContents.GetContent<AutoApprovalEnrolmentResponse>(response);
            return updateAutoApprovedEnrolledUser;
        }
    }
}
