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
    public class PostAgentPDOOperator
    {   //Helper class constractor 

        public AgentPDOHelper _pDOAgentHelper;
        public Helpers _helpers;
        public PostAgentPDOOperator()
        {
            _pDOAgentHelper = new AgentPDOHelper();
            _helpers = new Helpers();

        }
        public AgentPDOResponse PostPdoAgentEnrolmentUserDetails(dynamic payload)
        {
            var client = _helpers.SetUrl("EnrolmentAppService/api/v1/Enrolment/AgentPDO");
            var request = _pDOAgentHelper.PostAgentPDOEnrolment(payload);
            request.RequestFormat = DataFormat.Json;
            var response = _helpers.GetResponse(client, request);
            var pdoAgentEnrolUser = HandleContents.GetContent<AgentPDOResponse>(response);
            return pdoAgentEnrolUser;
        }
    }
} 