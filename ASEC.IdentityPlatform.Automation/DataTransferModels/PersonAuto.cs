using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAWTrust_API_Automation.DataTransferModels
{
   public class PersonAuto
    {
        public Guid PersonId { get; set; }
        public Guid SessionId { get; set; }
        public Guid ClientId { get; set; }
        public object ClientProductId { get; set; }
        public string IdentityNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
    }
}
