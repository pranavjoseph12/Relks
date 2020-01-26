using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CallDetails
    {
        public string CallType { get; set; }
        public string AgentName { get; set; }
        public int Duration { get; set; }
        public string CallTime { get; set; }
        public string Recording { get; set; }
    }
}
