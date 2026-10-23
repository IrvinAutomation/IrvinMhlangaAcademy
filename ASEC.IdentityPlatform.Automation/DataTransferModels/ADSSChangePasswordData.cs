using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAWTrust_API_Automation.DataTransferModels
{
    public class ADSSChangePasswordData
    {
        public string Message { get; set; }
        public string UserFriendlyMessage { get; set; }
        public long Result { get; set; }
        public string Identifier { get; set; }
    }
}
