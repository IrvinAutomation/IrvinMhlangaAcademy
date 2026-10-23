using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAWTrust_API_Automation.DataTransferModels
{
    public class Product
    {
        public Guid ClientProductId { get; set; }
        public string DisplayText { get; set; }
        public string Agreement { get; set; }
        //public object ProductId { get; set; }
    }
}
